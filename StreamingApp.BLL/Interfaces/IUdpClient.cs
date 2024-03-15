namespace StreamingApp.BLL.Interfaces;

public interface IUdpClient
{
    void Connect(IConfig config);
    Task SendAsync(byte[] buffer);
}
