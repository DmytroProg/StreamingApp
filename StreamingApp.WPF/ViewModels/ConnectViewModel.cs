using GalaSoft.MvvmLight.Command;
using StreamingApp.Networking.Configs;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels.Base;
using System.Windows.Input;

namespace StreamingApp.WPF.ViewModels;

internal class ConnectViewModel : ViewModelBase
{
    private string _meetingCode;
    private NavigationStore _navigationStore;

    public string MeetingCode {
        get => _meetingCode;
        set
        {
            _meetingCode = value;
            OnPropertyChanged();
        }
    }

    public ICommand ConnectCommand { get; }
    public ICommand CreateCommand { get; }

    public ConnectViewModel(NavigationStore navigationStore) {
        _navigationStore = navigationStore;
        ConnectCommand = new RelayCommand(Connect);
        CreateCommand = new NavigationCommand(
            new NavigationService(_navigationStore, () => new CreateMeetingViewModel()));
    }

    public void Connect()
    {
        //var tcpConfig = new TcpConfig()
        //{
        //    IPAddress = System.Net.IPAddress.Parse(IPAddress.Trim()),
        //    Port = Port
        //};
        App.UserController.Connect(MeetingCode);
    }
}
