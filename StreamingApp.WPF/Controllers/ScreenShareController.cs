using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Responses;
using StreamingApp.WPF.Controllers.Base;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
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
        BitmapEncoder encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
        MemoryStream ms = new MemoryStream();
        encoder.Save(ms);

        ScreenFrame screen = new ScreenFrame()
        {
            Frame = ms.ToArray(),
        };

        return ms.ToArray();
        //_presenter.ChangeView(new SendScreenResponse() { Screen = screen});
        // write to stream
    }
}
