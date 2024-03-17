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
    private int _port;

    public int Port => _port;

    public event Action<byte[]>? Received;

    public void Connect(IConfig config)
    {
        if (config is IPConfig ipConfig)
        {
            _endPoint = new IPEndPoint(ipConfig.IPAddress, ipConfig.Port);
            // TODO: change to the safer way

            _port = _endPoint.Port + new Random().Next(1, 9999);
            _udpClient = new UdpClient(_port);
        }
        else throw new NotImplementedException();

        Thread thread = new Thread(Listen);
        thread.IsBackground = true;
        thread.Start();
    }

    public void Send(byte[] buffer)
    {
        Debug.WriteLine($"Client sends: {buffer.Length} bytes");
        _udpClient?.Send(buffer, Math.Min(MaxBufferSize, buffer.Length), _endPoint);
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
            if (_udpClient is null) continue;
            var buffer = _udpClient.Receive(ref _endPoint);
            Debug.WriteLine($"Client receives: {buffer.Length} bytes");
            Received?.Invoke(buffer);
        }
    }
}
