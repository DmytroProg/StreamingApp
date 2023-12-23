using GalaSoft.MvvmLight.Command;
using StreamingApp.WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StreamingApp.WPF.ViewModels;

class LoginViewModel : ViewModelBase
{
    private string _login;
    private string _password;

    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel() {
        LoginCommand = new RelayCommand(LoginUser);
    }

    public void LoginUser()
    {
        App.UserController.Login(Login, Password);
    }
}
