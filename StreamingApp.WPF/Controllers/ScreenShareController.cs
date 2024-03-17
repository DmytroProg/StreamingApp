using Networking;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Requests;
using StreamingApp.WPF.Controllers.Base;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace StreamingApp.WPF.Controllers;

public class ScreenShareController : ControllerBase
{
    private const int SleepTime = 1000;
    private readonly IUdpClient _udpClient;

    public ScreenShareController(ILogger? logger, ITcpClient tcpClient, 
        IScreenSharePresenter presenter, IUdpClient udpClient)
        : base(logger, tcpClient, presenter)
    {
        _udpClient = udpClient;
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
        try
        {
            var config = NetworkConfiguration.GetStaticConfig(9999);
            var request = new StartSharingRequest()
            {
                SenderId = UserInfo.CurrentUser.Id,
                MeetingId = UserInfo.MeetingId,
                IpAddress = config.IPAddress.ToString(),
                Port = config.Port,
            };

            await _tcpClient.SendRequestAsync(request);
        }
        catch(Exception ex)
        {
            _logger?.LogError(ex);
        }
    }

    public async Task SendScreenAsync()
    {
        try
        {
            var config = NetworkConfiguration.GetStaticConfig(9999);
            _udpClient.Connect(config);
            while (true)
            {
                _udpClient.Send(GetFrame());
                await Task.Delay(SleepTime);
            }
        }
        catch(Exception ex)
        {
            _logger?.LogError(ex);
        }
    }
}
