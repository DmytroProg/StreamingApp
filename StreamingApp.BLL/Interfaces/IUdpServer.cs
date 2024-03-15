namespace StreamingApp.BLL.Interfaces;

public interface IUdpServer
{
    Task Connect(int port);

    event Action<byte[]>? Received;
}
