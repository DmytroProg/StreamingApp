using StreamingApp.BLL.Interfaces;
using StreamingApp.Networking.Configs;
using System.Net.Sockets;

namespace Networking.Network;

public class UdpClientServer : IUdpServer
{
    private UdpClient? _udpClient;

    public event Action<byte[]>? Received;

    public async Task Connect(int port)
    {
        _udpClient = new UdpClient(port);
        await Receive();
    }

    public async Task Receive()
    {
        if (_udpClient is null)
            throw new NullReferenceException(nameof(_udpClient));

        while (true)
        {
            var result = await _udpClient.ReceiveAsync();

            Received?.Invoke(result.Buffer);
        }
    }
}
