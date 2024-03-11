using GalaSoft.MvvmLight.Command;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels.Base;
using System.Windows.Input;

namespace StreamingApp.WPF.ViewModels;

class LoginViewModel : ViewModelBase
{
    private string _login;
    private string _password;
    private NavigationStore _navigationStore;

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
    public ICommand GoToRegistrationCommand { get; }

    public LoginViewModel(NavigationStore navigationStore) {
        _navigationStore = navigationStore;
        LoginCommand = new RelayCommand(LoginUser);
        GoToRegistrationCommand = new NavigationCommand(
            new NavigationService(_navigationStore, () => new RegistrationViewModel()));
    }

    public void LoginUser()
    {
        App.UnitController.UserController.Login(Login, Password);
    }
}
