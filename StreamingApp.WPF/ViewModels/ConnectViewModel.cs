using GalaSoft.MvvmLight.Command;
using StreamingApp.Networking.Configs;
using StreamingApp.WPF.ViewModels.Base;
using System.Windows.Input;

namespace StreamingApp.WPF.ViewModels;

internal class ConnectViewModel : ViewModelBase
{
    private string _iPAddress;
    private int _port;

    public string IPAddress {
        get => _iPAddress;
        set
        {
            _iPAddress = value;
            OnPropertyChanged();
        }
    }

    public int Port
    {
        get => _port;
        set
        {
            _port = value;
            OnPropertyChanged();
        }
    }

    public ICommand ConnectCommand { get; }

    public ConnectViewModel() {
        ConnectCommand = new RelayCommand(Connect);
    }

    public void Connect()
    {
        var tcpConfig = new TcpConfig()
        {
            IPAddress = System.Net.IPAddress.Parse(IPAddress.Trim()),
            Port = Port
        };
        App.UserController.Connect(tcpConfig);
    }
}
