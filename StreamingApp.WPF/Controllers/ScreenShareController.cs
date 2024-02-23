using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.WPF.Controllers.Base;
using System.IO;
using System.Windows.Media.Imaging;

namespace StreamingApp.WPF.Controllers;

public class ScreenShareController : ControllerBase
{
    public ScreenShareController(ITcpClient tcpClient, IScreenSharePresenter presenter) 
        : base(tcpClient, presenter)
    {
    }

    public byte[] SendFrame()
    {
        var bitmapSource = ScreenLib.ScreenHelper.GetScreenSource();
        var encoder = new JpegBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
        var ms = new MemoryStream();
        encoder.Save(ms);

        //ScreenFrame screen = new ScreenFrame()
        //{
        //    Frame = ms.ToArray(),
        //};

        return ms.ToArray();
        // write to stream
    }
}
