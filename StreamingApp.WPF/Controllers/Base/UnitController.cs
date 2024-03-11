using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StreamingApp.WPF.Controllers.Base;

public class UnitController
{
    private readonly UserController _userController;
    private readonly MessageController _messageController;
    private readonly ScreenShareController _screenController;

    public UnitController(IHost host)
    {
        _userController = host.Services.GetRequiredService<UserController>();
        _messageController = host.Services.GetRequiredService<MessageController>();
        _screenController = host.Services.GetRequiredService<ScreenShareController>();
    }

    public UserController UserController {
        get
        {
            _userController.SetSender();
            return _userController;
        }
    }

    public MessageController MessageController { 
        get {
            _messageController.SetSender();
            return _messageController;
        }
    }

    public ScreenShareController ScreenShareController {
        get
        {
            _screenController.SetSender();
            return _screenController;
        }
    }
}
