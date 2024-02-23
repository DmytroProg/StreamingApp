using StreamingApp.WPF.ViewModels.Base;
using System.Threading;
using System.Windows.Media;

namespace StreamingApp.WPF.ViewModels;

internal class ScreenShareViewModel : ViewModelBase
{
    private const int SleepTime = 100;

    private readonly ImageSourceConverter _converter;
    private readonly Thread _updateScreenThread;
    private ImageSource? _imageSource;

    public ScreenShareViewModel()
    {
        CurrentScreen = null;
        _converter = new ImageSourceConverter();
        _updateScreenThread = new Thread(UpdateScreen)
        {
            IsBackground = true,
        };
        _updateScreenThread.Start();
    }
    
    public ImageSource? CurrentScreen  {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged();
        }
    }

    private void UpdateScreen()
    {
        while (true)
        {
            var buffer = App.ScreenShareController.SendFrame();
            CurrentScreen = (ImageSource?)_converter.ConvertFrom(buffer);
            
            Thread.Sleep(SleepTime);
        }
    }
}
