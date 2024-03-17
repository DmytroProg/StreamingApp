using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Interfaces.Services;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using StreamingApp.BLL.Services;
using System.Net.Sockets;

namespace StreamingApp.BLL.UseCase;

public class UseCaseInteractor
{
    private readonly ITcpServer _tcpServer;
    private readonly IUdpServer _udpServer;
    private readonly Dictionary<int, TcpClient> _clients;
    private readonly IUserService _userService;
    private readonly IMeetingService _meetingService;
    private readonly ILogger _logger;

    private List<TcpClient> _sendClients;

    public UseCaseInteractor(ITcpServer tcpServer, IUdpServer udpServer,
        IUserRepository userRepository,
        IMeetingRepository meetRepository, ILogger logger)
    {
        _tcpServer = tcpServer;
        _udpServer = udpServer;
        _tcpServer.RequestReceived += _tcpServer_Received;
        _clients = new();
        _meetingService = new MeetingService(meetRepository, logger);
        _userService = new UserService(userRepository, logger);
        _logger = logger;
    }

    public event Action<int>? UserCountChanged;

    public async Task ConnectAsync(IConfig config)
    {
        try
        {
            await _tcpServer.StartListenAsync(config);
        }
        catch(Exception ex) {
            _logger.LogError(ex);
            throw;
        }
    }

    private async Task _tcpServer_Received(RequestBase request, TcpClient client)
    {
        _sendClients = new();
        ResponseBase response = request switch
        {
            LoginRequest loginReq => await OnLogin(loginReq, client),
            LogoutRequest logoutReq => OnLogout(logoutReq),
            RegisterRequest registerReq => await OnRegister(registerReq, client),
            ConnectRequest connectReq => await OnConnect(connectReq, client),
            CreateMeetingRequest createReq => await OnCreateMeeting(createReq, client),
            SendMessageRequest sendReq => await OnMessageSend(sendReq, client),
            LeaveMeetingRequest leaveReq => await OnLeaveMeeting(leaveReq, client),
            StartSharingRequest shareReq => await OnStartSharing(shareReq, client),
            _ => new ErrorResponse(),
        };

        if(_sendClients.Count == 0)
        {
            if (request is LogoutRequest) return;
            _sendClients = _clients.Values.ToList();
        }
        
        if (response is StartSharingResponse)
        {
            await _tcpServer.SendResponseAsync(client, response);
        }

        foreach (var tcpClient in _sendClients)
        { 
            await _tcpServer.SendResponseAsync(tcpClient, response);
        }
    }

    private async Task<ResponseBase> OnStartSharing(StartSharingRequest shareReq, TcpClient client)
    {
        try
        {
            var meeting = await _meetingService.GetByIdAsync(shareReq.MeetingId);
            var user = meeting.Users.FirstOrDefault(u => u.Id == shareReq.SenderId);
            if (user is null) return new ErrorResponse();

            _sendClients.Clear();
            foreach(var receiver in meeting.Users)
            {
                if(receiver.Id == shareReq.SenderId) continue;
                _sendClients.Add(_clients[receiver.Id]);
            }

            _udpServer.Connect(9999);

            return new StartSharingResponse()
            {
                SenderId = shareReq.SenderId,
            };
        }
        catch(Exception ex)
        {
            _logger.LogError(ex);
            return new ErrorResponse();
        }
    }

    private async Task<ResponseBase> OnLeaveMeeting(LeaveMeetingRequest leaveReq, TcpClient client)
    {
        try
        {
            var meeting = await _meetingService.GetByIdAsync(leaveReq.MeetingId);
            var user = meeting.Users.FirstOrDefault(u => u.Id == leaveReq.User.Id);
            if (user is null) return new ErrorResponse();

            _udpServer.ClientsPorts.Remove(leaveReq.SharingPort);
            meeting.Users.Remove(user);
            _sendClients.Clear();
            foreach(var meetingUser in meeting.Users)
            {
                _sendClients.Add(_clients[meetingUser.Id]);
            }

            return new LeaveMeetingResponse()
            {
                LeftUser = user,
            };
        }
        catch(Exception ex)
        {
            _logger.LogError(ex);
            return new ErrorResponse();
        }
    }

    private async Task<ResponseBase> OnMessageSend(SendMessageRequest sendReq, TcpClient client)
    {
        try
        {
            int? userId = _clients.Keys.FirstOrDefault(id => sendReq.Sender.Id == id);
            if (userId is null) return new ErrorResponse();

            var meeting = await _meetingService.GetByIdAsync(sendReq.MeetingId);

            _sendClients.Clear();
            foreach (var user in meeting.Users)
            {
                _sendClients.Add(_clients[user.Id]);
            }

            return new MessageResponse()
            {
                Sender = sendReq.Sender,
                Message = sendReq.Message,
            };
        }
        catch(Exception ex)
        {
            _logger.LogError(ex);
            return new ErrorResponse();
        }
    }

    private async Task<ResponseBase> OnCreateMeeting(CreateMeetingRequest createReq, TcpClient client)
    {
        try
        {
            var admin = await _userService.GetByIdAsync(createReq.Meeting.AdminId);
            var meeting = await _meetingService.AddAsync(createReq.Meeting);
            await _meetingService.AddUserToMeetingAsync(meeting.Id, admin);
            _sendClients = new() { client };
            _udpServer.ClientsPorts.Add(createReq.SharingPort);

            return new CreateMeetingResponse()
            {
                Meeting = meeting,
            };
        }
        catch(Exception ex)
        {
            _logger.LogError(ex);
            return new ErrorResponse();
        }
    }

    private async Task<ResponseBase> OnConnect(ConnectRequest connectReq, TcpClient client)
    {
        try
        {
            //var meeting = _meetings.FirstOrDefault(m => m.Id == connectReq.MeetingCode);
            var meeting = await _meetingService.GetMeetingByCode(connectReq.MeetingCode);
            _sendClients = new() { client };
            await _meetingService.AddUserToMeetingAsync(meeting.Id, connectReq.User);
            _udpServer.ClientsPorts.Add(connectReq.SharingPort);

            return new ConnectResponse()
            {
                ConnectedUser = connectReq.User,
                Meeting = meeting,
            };
        }
        catch(Exception ex)
        {
            _logger.LogError(ex);
            return new ErrorResponse();
        }
    }

    private async Task<ResponseBase> OnRegister(RegisterRequest registerReq, TcpClient client)
    {
        try
        {
            var user = await _userService.AddAsync(registerReq.User);
            _clients.Add(user.Id, client);
            _sendClients.Add(client);
            UserCountChanged?.Invoke(_clients.Count);
            return new LoginResponse()
            {
                User = user,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
            return new ErrorResponse();
        }
    }

    private ResponseBase OnLogout(LogoutRequest logoutReq)
    {
        try
        {
            _clients.Remove(logoutReq.UserId);
            UserCountChanged?.Invoke(_clients.Count);
            return null;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex);
            return new ErrorResponse();
        }
    }

    private async Task<ResponseBase> OnLogin(LoginRequest loginReq, TcpClient client)
    {
        try
        {
            var user = await _userService.GetByLoginAsync(loginReq.Login, loginReq.Password);

            _clients.Add(user.Id, client);
            _sendClients.Add(client);
            UserCountChanged?.Invoke(_clients.Count);
            return new LoginResponse()
            {
                User = user,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
            return new ErrorResponse();
        }
    }
}
