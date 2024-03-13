using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using System.Net.Sockets;

namespace StreamingApp.BLL.Interfaces;

public interface ITcpServer
{
    Task StartListenAsync(IConfig config);
    Task StartListenAsync(string ipAddress, int port);
    Task SendResponseAsync(TcpClient client, ResponseBase response);
    event Func<RequestBase, TcpClient, Task>? RequestReceived;
}
