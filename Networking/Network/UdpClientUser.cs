using StreamingApp.BLL.Interfaces;
using StreamingApp.Networking.Configs;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Networking.Network;

public class UdpClientUser : IUdpClient
{
    private UdpClient? _udpClient;
    private IPEndPoint? _endPoint;
    private const int MaxBufferSize = 62 * 1024;

    public event Action<byte[]>? Received;

    public void Connect(IConfig config)
    {
        if (config is IPConfig ipConfig)
        {
            _endPoint = new IPEndPoint(ipConfig.IPAddress, ipConfig.Port);

            _udpClient = new UdpClient();
            _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _udpClient.Client.Bind(_endPoint);

            _udpClient.JoinMulticastGroup(IPAddress.Parse("239.0.0.1"));
        }
        else throw new NotImplementedException();

        Thread thread = new Thread(Listen);
        thread.IsBackground = true;
        thread.Start();
    }

    public void Send(byte[] buffer)
    {
        using var udpClient = new UdpClient();
        udpClient.JoinMulticastGroup(IPAddress.Parse("239.0.0.1"));
        var endPoint = new IPEndPoint(IPAddress.Parse("239.0.0.1"), 9999);
        Debug.WriteLine($"Client sends: {buffer.Length} bytes | endPoint: {endPoint.Address}:{_endPoint.Port}");
        udpClient.Send(buffer, Math.Min(MaxBufferSize, buffer.Length), endPoint);
        //for (long i = 0; i < buffer.Length; i += MaxBufferSize)
        //{

        //    //udpClient.Send(buffer.ToArray(), 
        //    //    buffer.Length > MaxBufferSize ? MaxBufferSize : buffer.Length,
        //    //    _endPoint);
        //}
    }

    private void Listen()
    {
        while (true)
        {
            var buffer = _udpClient.Receive(ref _endPoint);
            Debug.WriteLine($"Client receives: {buffer.Length} bytes | ref endPoint: {_endPoint.Address}:{_endPoint.Port}");
            Received?.Invoke(buffer);
        }
    }
}
