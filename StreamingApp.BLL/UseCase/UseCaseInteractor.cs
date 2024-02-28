using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;

namespace StreamingApp.BLL.UseCase;

public class UseCaseInteractor
{
    private readonly ITcpServer _tcpServer;
    private readonly Dictionary<User, IConfig> _users;
    private readonly List<Meeting> _meetings;

    private readonly IService<User> _userService;

    public UseCaseInteractor(ITcpServer tcpServer)
    {
        _tcpServer = tcpServer;
        _tcpServer.Received += _tcpServer_Received;
        _users = new();
        _meetings = new();
    }

    public async Task Connect(IConfig config)
    {
        await _tcpServer.ConnectAsync(config);
    }
    private async Task _tcpServer_Received(RequestBase request)
    {
        ResponseBase response = request switch
        {
            LoginRequest loginReq => await OnLogin(loginReq),

            _ => throw new Exception(),
        };
        
        await _tcpServer.SendResponseAsync(response);
    }

    private async Task<LoginResponse> OnLogin(LoginRequest loginReq)
    {
        var user = await _userService.QueryOne(user => user.Login == loginReq.Login &&
        user.Password == loginReq.Password);
        return new LoginResponse()
        {
            User = user,
        };
    }
}
