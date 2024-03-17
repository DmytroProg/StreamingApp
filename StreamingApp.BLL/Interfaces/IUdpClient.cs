namespace StreamingApp.BLL.Interfaces;

public interface IUdpClient
{
    int Port { get; }
    void Connect(IConfig config);
    void Send(byte[] buffer);

    event Action<byte[]>? Received;
}
