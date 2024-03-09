using StreamingApp.WPF.Controllers.Base;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Responses;
using System;
using System.Collections.Generic;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Requests;
using System.Threading.Tasks;
using StreamingApp.Networking.Configs;
using System.Net;
using System.Linq;

namespace StreamingApp.WPF.Controllers;

public class UserController : ControllerBase
{
    public static User CurrentUser { get; private set; } = null!;

    public UserController(ITcpClient tcpClient, IUserPresenter presenter) 
        : this(null, tcpClient, presenter)
    {
    }

    public UserController(ILogger? logger, ITcpClient tcpClient, IUserPresenter presenter)
        : base(logger, tcpClient, presenter)
    {
    }

    public async Task Login(string login, string password)
    {
        try
        {
            //TODO validation
            //Send request to server and get response
            _currentSender = this;
            var hostName = Dns.GetHostName();
            var hostAddress = Dns.GetHostAddresses(hostName);
            await _tcpClient.ConnectAsync(new TcpConfig()
            {
                IPAddress = hostAddress.FirstOrDefault(ip => ip.AddressFamily
            == System.Net.Sockets.AddressFamily.InterNetwork)!,
                Port = 8888,
            });

            await _tcpClient.SendRequestAsync(new LoginRequest()
            {
                Login = login,
                Password = password
            });

            //var response = new LoginResponse()
            //{
            //    User = new User()
            //    {
            //        Id = 1,
            //        Login = login,
            //        Password = password,
            //        Name = "Test"
            //    }
            //};
            //_presenter.ChangeView(response);
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
            //TODO validation
            //Create request
            //Send request to server and get response
            _currentSender = this;
            var hostName = Dns.GetHostName();
            var hostAddress = Dns.GetHostAddresses(hostName);
            await _tcpClient.ConnectAsync(new TcpConfig()
            {
                IPAddress = hostAddress.FirstOrDefault(ip => ip.AddressFamily
            == System.Net.Sockets.AddressFamily.InterNetwork)!,
                Port = 8888,
            });

            CurrentUser = user;
            await _tcpClient.SendRequestAsync(new RegisterRequest()
            {
                User = user,
            });
            //var response = new LoginResponse()
            //{
            //    User = new User()
            //    {
            //        Login = user.Login,
            //        Password = user.Password,
            //        Name = user.Name
            //    }
            //};
            //_presenter.ChangeView(response);
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
            //TODO validation
            //Create request
            //Send request to server and get response
            _currentSender = this;
            var request = new ConnectRequest()
            {
                User = CurrentUser,
                MeetingCode = meetingCode,
            };

            //var response = new ConnectResponse()
            //{
            //    Meeting = new Meeting()
            //    {
            //        Title = "Test meeting",
            //        Admin = CurrentUser!,
            //        Messages = new List<MessageBase>(),
            //        Users = new List<User>()
            //    }
            //};
            //_presenter.ChangeView(response);
            await _tcpClient.SendRequestAsync(request);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex);
        }
    }
}