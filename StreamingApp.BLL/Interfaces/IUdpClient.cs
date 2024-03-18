namespace StreamingApp.BLL.Interfaces;

public interface IUdpClient
{
    void Connect(IConfig config);
    void Send(byte[] buffer);

    event Action<byte[]>? Received;
}
