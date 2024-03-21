namespace StreamingApp.BLL.Interfaces;

public interface IUdpClient
{
    void Connect(IConfig config, byte segmentsCount);
    void Send(byte[] buffer);
    void Close();

    event Action<byte[]>? Received;
}
