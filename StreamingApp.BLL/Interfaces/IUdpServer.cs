namespace StreamingApp.BLL.Interfaces;

public interface IUdpServer
{
    List<int> ClientsPorts { get; set; }
    void Connect(int port);
    void Listen();

    event Action<byte[]>? Received;
}
