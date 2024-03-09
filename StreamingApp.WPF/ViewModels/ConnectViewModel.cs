using GalaSoft.MvvmLight.Command;
using StreamingApp.Networking.Configs;
using StreamingApp.WPF.ViewModels.Base;
using System.Windows.Input;

namespace StreamingApp.WPF.ViewModels;

internal class ConnectViewModel : ViewModelBase
{
    private string _meetingCode;

    public string MeetingCode {
        get => _meetingCode;
        set
        {
            _meetingCode = value;
            OnPropertyChanged();
        }
    }

    public ICommand ConnectCommand { get; }

    public ConnectViewModel() {
        ConnectCommand = new RelayCommand(Connect);
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
