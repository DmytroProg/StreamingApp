using StreamingApp.WPF.Controllers.Base;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Responses;
using System;
using System.Collections.Generic;

namespace StreamingApp.WPF.Controllers;

public class UserController : ControllerBase
{
    public static User CurrentUser { get; private set; } = null!;

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

    public void Register(User user)
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
                    Login = user.Login,
                    Password = user.Password,
                    Name = user.Name
                }
            };
            _presenter.ChangeView(response);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex);
        }
    }

    public void Connect(TcpConfig config)
    {
        try
        {
            //TODO validation
            //Create request
            //Send request to server and get response
            var response = new ConnectResponse()
            {
                Meeting = new Meeting()
                {
                    Title = "Test meeting",
                    Admin = CurrentUser!,
                    Messages = new List<MessageBase>(),
                    Users = new List<User>()
                }
            };
            _presenter.ChangeView(response);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex);
        }
    }
}