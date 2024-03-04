using GalaSoft.MvvmLight.Command;
using StreamingApp.BLL.Models;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels.Base;
using System.Windows.Input;

namespace StreamingApp.WPF.ViewModels;

internal class RegistrationViewModel : ViewModelBase
{
    private string _nickname;
    private string _login;
    private string _password;

    public string Nickname
    {
        get => _nickname;
        set
        {
            _nickname = value;
            OnPropertyChanged();
        }
    }

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

    public ICommand RegisterCommand { get; }

    public RegistrationViewModel() {
        RegisterCommand = new RelayCommand(Register);
    }

    public void Register()
    {
        var user = new User() { 
            Name = Nickname,
            Login = Login,
            Password = Password,
            AvatarImage = "img"
        };

        App.UserController.Register(user);
    }
}
