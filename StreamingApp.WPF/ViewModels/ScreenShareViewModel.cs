using StreamingApp.BLL.Interfaces;
using StreamingApp.WPF.ViewModels.Base;
using System.Windows.Media;

namespace StreamingApp.WPF.ViewModels;

internal class ScreenShareViewModel : ViewModelBase
{
    private ImageSource? _imageSource;
    private ImageSourceConverter _converter;
    private readonly ITcpClient _tcpClient;

    public ScreenShareViewModel(ITcpClient tcpClient)
    {
        _tcpClient = tcpClient;
        //_tcpClient.Received += _tcpClient_Received;
        _converter = new ImageSourceConverter();
    }

    private void _tcpClient_Received(byte[] buffer)
    {
        //CurrentScreen = (ImageSource?)_converter.ConvertFrom(buffer);
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
