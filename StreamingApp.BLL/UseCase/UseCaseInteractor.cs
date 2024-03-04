using StreamingApp.BLL.Interfaces;
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
    private readonly List<Meeting> _meetings;
    private readonly IService<User> _userService;
    private readonly ILogger _logger;

    private List<TcpClient> _sendClients;

    public UseCaseInteractor(ITcpServer tcpServer, ILogger logger)
    {
        _tcpServer = tcpServer;
        _tcpServer.RequestReceived += _tcpServer_Received;
        _clients = new();
        _meetings = new();
        _userService = new UserServise();
        _logger = logger;
    }

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
            _ => throw new Exception(),
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

    private async Task<ConnectResponse> OnConnect(ConnectRequest connectReq, TcpClient client)
    {
        //var meeting = _meetings.FirstOrDefault(m => m.Id == connectReq.MeetingCode);
        var meeting = new Meeting()
        {
            Id = 0,
            Admin = new(),
            Messages = new(),
            Title = "Test Meeting",
            UserId = 0,
            Users = new(),
        };
        _sendClients = new() { client };

        return new ConnectResponse()
        {
            Meeting = meeting,
        };
    }

    private async Task<LoginResponse> OnRegister(RegisterRequest registerReq, TcpClient client)
    {
        try
        {
            await _userService.AddAsync(registerReq.User);
            return await Login(registerReq.User, client);
        }
        catch (Exception ex)
        {
            int a = 0;
            return null;
        }
    }

    private Task<ResponseBase> OnLogout(LogoutRequest logoutReq)
    {
        _clients.Remove(logoutReq.UserId);
        return null;
    }

    private Task<LoginResponse> OnLogin(LoginRequest loginReq, TcpClient client)
    {
        return Login(new() { Login = loginReq.Login, Password = loginReq.Password }, client);
    }

    private async Task<LoginResponse> Login(User requestedUser, TcpClient client)
    {
        var user = await _userService.QueryOne(user => user.Login == requestedUser.Login &&
        user.Password == requestedUser.Password);

        _clients.Add(user.Id, client);
        _sendClients.Add(client);
        return new LoginResponse()
        {
            User = user,
        };
    }
}
