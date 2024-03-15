using StreamingApp.BLL.Interfaces;
using StreamingApp.Networking.Configs;
using System.Net;
using System.Net.Sockets;

namespace Networking.Network;

public class UdpClientUser : IUdpClient
{
    private UdpClient? _udpClient;
    private const int MaxBufferSize = 62 * 1024;
    public void Connect(IConfig config)
    {
        if (config is IPConfig ipConfig)
        {
            _udpClient = new UdpClient(new IPEndPoint(ipConfig.IPAddress, ipConfig.Port));
        }
        else throw new NotImplementedException();
    }

    public async Task SendAsync(byte[] buffer)
    {
        if(_udpClient is null)
            throw new NullReferenceException(nameof(_udpClient));

        for (long i = 0; i < buffer.Length; i += MaxBufferSize)
        {
            await _udpClient.SendAsync(buffer.ToArray(), 
                buffer.Length > MaxBufferSize ? MaxBufferSize : buffer.Length);
        }
    }
}
