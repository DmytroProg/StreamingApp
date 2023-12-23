using StreamingApp.BLL.Controllers.Base;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Responses;

namespace StreamingApp.BLL.Controllers;

public class UserController : ControllerBase
{
    public static User? CurrentUser { get; private set; }

    public UserController(ITcpClient tcpClient, IPresenter presenter) 
        : this(null, tcpClient, presenter)
    {
    }

    public UserController(ILogger? logger, ITcpClient tcpClient, IPresenter presenter)
        : base(logger, tcpClient, presenter)
    {
    }

    public void Login(string login, string password)
    {
        try
        {
            //TODO validation
            //Create request
            //Send request to server and get response
            var response = new LoginResponse()
            {
                User = new User()
                {
                    Id = 1,
                    Login = login,
                    Password = password,
                    Name = "Test"
                }
            };
            _presenter.ChangeView(response);
        }
        catch(Exception ex)
        {
            _logger?.LogError(ex);
        }
    }
}
