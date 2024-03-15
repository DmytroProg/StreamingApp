using StreamingApp.BLL.Interfaces;
using StreamingApp.WPF.ViewModels.Base;
using System;
using System.Windows.Media;

namespace StreamingApp.WPF.ViewModels;

internal class ScreenShareViewModel : ViewModelBase
{
    private ImageSource? _imageSource;
    private ImageSourceConverter _converter;
    private readonly IUdpServer _udpServer;

    public ScreenShareViewModel(IUdpServer udpServer)
    {
        _converter = new ImageSourceConverter();
        _udpServer = udpServer;
        _udpServer.Received += _udpServer_Received;
        _udpServer.Connect(9999);
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
