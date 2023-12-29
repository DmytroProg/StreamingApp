using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using System.Net.Sockets;

namespace StreamingApp.BLL.Interfaces;

public interface ITcpClient
{
    Task SendRequestAsync(RequestBase request, TcpClient client);
    Task ConnectAsync(IConfig config);

    event Action<ResponseBase> Received; 
}
