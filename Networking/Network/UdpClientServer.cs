using StreamingApp.BLL.Interfaces;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Networking.Network;

public class UdpClientServer : IUdpServer
{
    private UdpClient? _udpClient;
    private IPEndPoint? _endPoint;

    public List<int> ClientsPorts { get; set; }

    public event Action<byte[]>? Received;

    public UdpClientServer()
    {
        var config = NetworkConfiguration.GetStaticConfig(9999);
        _endPoint = new IPEndPoint(config.IPAddress, 9999);
        ClientsPorts = new List<int>();
    }

    public void Connect(int port)
    {
        _udpClient = new UdpClient(_endPoint.Port);
        Thread thread = new Thread(Listen);
        thread.IsBackground = true;
        thread.Start();
    }

    public void Listen()
    {
        while (true)
        {
            if (_udpClient is null) continue;
            var buffer = _udpClient.Receive(ref _endPoint);
            Debug.WriteLine($"Server receives: {buffer.Length} bytes");
            Received?.Invoke(buffer);

            foreach(var port in ClientsPorts)
            {
                if (port == _endPoint.Port) continue;
                _udpClient.Send(buffer, buffer.Length, 
                    new IPEndPoint(_endPoint.Address, port));
            }
        }
    }
}
