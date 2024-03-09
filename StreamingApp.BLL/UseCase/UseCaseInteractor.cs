using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Interfaces.Services;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using StreamingApp.BLL.Services;
using System.Net.Sockets;

namespace StreamingApp.BLL.UseCase;

public class UseCaseInteractor
{
    private readonly ITcpServer _tcpServer;
    private readonly Dictionary<int, TcpClient> _clients;
    private readonly IUserService _userService;
    private readonly IMeetingService _meetingService;
    private readonly ILogger _logger;

    private List<TcpClient> _sendClients;

    public UseCaseInteractor(ITcpServer tcpServer, IUserRepository userRepository,
        IMeetingRepository meetRepository, ILogger logger)
    {
        _tcpServer = tcpServer;
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
        }
    }
    private async Task _tcpServer_Received(RequestBase request, TcpClient client)
    {
        _sendClients = new();
        ResponseBase response = request switch
        {
            LoginRequest loginReq => await OnLogin(loginReq, client),
            LogoutRequest logoutReq => await OnLogout(logoutReq),
            RegisterRequest registerReq => await OnRegister(registerReq, client),
            ConnectRequest connectReq => await OnConnect(connectReq, client),
            CreateMeeting createReq => await OnCreateMeeting(createReq, client),

            _ => new ErrorResponse(),
        };

        if(_sendClients.Count == 0)
        {
            _sendClients = _clients.Values.ToList();
        }

        foreach (var tcpClient in _sendClients)
        { 
            await _tcpServer.SendResponseAsync(tcpClient, response);
        }
    }

    private async Task<CreateMeetingResponse> OnCreateMeeting(CreateMeeting createReq, TcpClient client)
    {
        var meeting = await _meetingService.AddAsync(createReq.Meeting);
        _sendClients = new() { client };

        return new CreateMeetingResponse()
        {
            Meeting = meeting,
        };
    }

    private async Task<ConnectResponse> OnConnect(ConnectRequest connectReq, TcpClient client)
    {
        //var meeting = _meetings.FirstOrDefault(m => m.Id == connectReq.MeetingCode);
        var meeting = await _meetingService.QueryOne(m => m.MeetingCode == connectReq.MeetingCode);
        _sendClients = new() { client };
        await _meetingService.AddUserToMeetingAsync(meeting.Id, connectReq.User);

        return new ConnectResponse()
        {
            Meeting = meeting,
        };
    }

    private async Task<LoginResponse> OnRegister(RegisterRequest registerReq, TcpClient client)
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
            return null;
        }
    }

    private Task<ResponseBase> OnLogout(LogoutRequest logoutReq)
    {
        _clients.Remove(logoutReq.UserId);
        UserCountChanged?.Invoke(_clients.Count);
        return null;
    }

    private async Task<LoginResponse> OnLogin(LoginRequest loginReq, TcpClient client)
    {
        var user = await _userService.QueryOne(user => user.Login == loginReq.Login &&
        user.Password == loginReq.Password);

        _clients.Add(user.Id, client);
        _sendClients.Add(client);
        UserCountChanged?.Invoke(_clients.Count);
        return new LoginResponse()
        {
            User = user,
        };
    }
}
