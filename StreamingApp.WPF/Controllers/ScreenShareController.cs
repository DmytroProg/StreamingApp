using Networking;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using StreamingApp.WPF.Controllers.Base;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace StreamingApp.WPF.Controllers;

public class ScreenShareController : ControllerBase
{
    private const int SleepTime = 300;

    public ScreenShareController(ILogger? logger, ITcpClient tcpClient, 
        IScreenSharePresenter presenter)
        : base(logger, tcpClient, presenter)
    {
    }

    public byte[] GetFrame()
    {
        var bitmapSource = ScreenLib.ScreenHelper.GetScreenSource();
        var encoder = new JpegBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
        var ms = new MemoryStream();
        encoder.Save(ms);

        return ms.ToArray();
    }

    public async Task StartSharing()
    {
        throw new NotImplementedException();
        try
        {
            //var config = NetworkConfiguration.GetStaticConfig(9999);
            //var request = new StartSharingRequest()
            //{
            //    SenderId = UserInfo.CurrentUser.Id,
            //    MeetingId = UserInfo.MeetingId,
            //    IpAddress = config.IPAddress.ToString(),
            //    Port = config.Port,
            //};

            //await _tcpClient.SendRequestAsync(request);
        }
        catch(Exception ex)
        {
            _logger?.LogError(ex);
        }
    }

    public async Task SendScreenAsync()
    {
        throw new NotImplementedException();
        while (true)
        {
            //await _frameClient.SendRequestAsync(GetFrame());
            //await Task.Delay(SleepTime);
        }
    }
}
