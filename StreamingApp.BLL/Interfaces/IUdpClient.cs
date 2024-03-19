namespace StreamingApp.BLL.Interfaces;

public interface IUdpClient
{
    void Connect(IConfig config, byte segmentsCount);
    void Send(byte[] buffer);

    event Action<byte[]>? Received;
}
