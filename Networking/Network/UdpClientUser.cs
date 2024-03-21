using StreamingApp.BLL.Interfaces;
using StreamingApp.Networking.Configs;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Networking.Network;

public class UdpClientUser : IUdpClient
{
    private UdpClient? _udpClient;
    private IPEndPoint? _endPoint;
    private readonly IFormatter _formatter;
    private readonly ConcurrentQueue<Segment> _segments;
    private byte _segmentsCount;

    public const int MaxBufferSize = 62 * 1024;
    public event Action<byte[]>? Received;

    public UdpClientUser()
    {
        _formatter = new BinaryFormatter();
        _segments = new ConcurrentQueue<Segment>();
    }

    public void Connect(IConfig config, byte segmentsCount)
    {
        if (config is IPConfig ipConfig)
        {
            _endPoint = new IPEndPoint(ipConfig.IPAddress, ipConfig.Port);
            _segmentsCount = segmentsCount;
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

        for(byte i = 0; i < _segmentsCount; i++)
        {
            var segment = new Segment()
            {
                Data = buffer.Skip(MaxBufferSize * i).Take(MaxBufferSize).ToArray(),
                Index = i,
            };

            using var ms = new MemoryStream();  
             _formatter.Serialize(ms, segment);

            udpClient.Send(ms.ToArray(), (int)ms.Length, endPoint);
            Thread.Sleep(60);
        }
        //udpClient.Send(buffer, Math.Min(MaxBufferSize, buffer.Length), endPoint);
    }

    private void Listen()
    {
        while (true)
        {
            var buffer = _udpClient.Receive(ref _endPoint);

            Segment segment = (Segment)_formatter.Deserialize(new MemoryStream(buffer));
            
            _segments.Enqueue(segment);
            Debug.WriteLine($"Client receives: {buffer.Length} bytes | ref endPoint: {_endPoint.Address}:{_endPoint.Port}");
            if(_segments.Count == _segmentsCount)
            {
                var data = _segments.SelectMany(s => s.Data).ToArray();
                Received?.Invoke(data);
                _segments.Clear();
            }
        }
    }

    public void Close()
    {
      _udpClient?.Close();
      _udpClient?.Dispose();
    }
}
