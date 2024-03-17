using StreamingApp.WPF.Controllers.Base;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Responses;
using System;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Requests;
using System.Threading.Tasks;
using StreamingApp.Networking.Configs;
using System.Net;
using System.Linq;
using Networking;

namespace StreamingApp.WPF.Controllers;

public class UserController : ControllerBase
{
    public UserController(ILogger? logger, ITcpClient tcpClient, IUdpClient udpClient,
        IUserPresenter presenter)
        : base(logger, tcpClient, udpClient, presenter)
    {
    }

    public async Task Login(string login, string password)
    {
        try
        {
            await _tcpClient.ConnectAsync(NetworkConfiguration.GetStaticConfig());

            await _tcpClient.SendRequestAsync(new LoginRequest()
            {
                Login = login,
                Password = password
            });
        }
        catch(ArgumentNullException ex)
        {
            _logger.LogError(ex);
            _presenter.ChangeView(new ErrorResponse());
        }
        catch(Exception ex)
        {
            _logger?.LogError(ex);
        }
    }

    public async Task Register(User user)
    {
        try
        {
            await _tcpClient.ConnectAsync(NetworkConfiguration.GetStaticConfig());

            UserInfo.CurrentUser = user;
            await _tcpClient.SendRequestAsync(new RegisterRequest()
            {
                User = user,
            });
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex);
        }
    }

    public async Task Connect(string meetingCode)
    {
        try
        {
            var request = new ConnectRequest()
            {
                User = UserInfo.CurrentUser,
                MeetingCode = meetingCode,
                SharingPort = _udpClient.Port
            };

            await _tcpClient.SendRequestAsync(request);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex);
        }
    }

    public async Task Logout()
    {
        try
        {
            var request = new LogoutRequest()
            {
                UserId = UserInfo.CurrentUser.Id,
            };
            await _tcpClient.SendRequestAsync(request);
        }
        catch(Exception ex)
        {
            _logger?.LogError(ex);
        }
    }

    public async Task CreateMeeting(Meeting meeting)
    {
        try
        {
            var request = new CreateMeetingRequest()
            {
                Meeting = meeting,
                SharingPort = _udpClient.Port
            };
            await _tcpClient.SendRequestAsync(request);
        }
        catch(Exception ex)
        {
            _logger?.LogError(ex);
        }
    }
}