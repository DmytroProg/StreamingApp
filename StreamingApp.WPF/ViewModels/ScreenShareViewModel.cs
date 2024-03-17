using Networking;
using StreamingApp.BLL.Interfaces;
using StreamingApp.WPF.ViewModels.Base;
using System;
using System.Windows.Media;

namespace StreamingApp.WPF.ViewModels;

internal class ScreenShareViewModel : ViewModelBase
{
    private ImageSource? _imageSource;
    private ImageSourceConverter _converter;
    private readonly IUdpClient _udpClient;

    public ScreenShareViewModel(IUdpClient udpServer)
    {
        _converter = new ImageSourceConverter();
        _udpClient = udpServer;
        _udpClient.Received += _udpServer_Received;
        var config = NetworkConfiguration.GetStaticConfig(9999);
        _udpClient.Connect(config);
    }

    private void _udpServer_Received(byte[] buffer)
    {
        CurrentScreen = (ImageSource?)_converter.ConvertFrom(buffer);
    }

    public ImageSource? CurrentScreen  {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged();
        }
    }
}
