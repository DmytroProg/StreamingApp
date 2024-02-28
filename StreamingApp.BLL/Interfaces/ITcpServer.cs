using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;

namespace StreamingApp.BLL.Interfaces;

public interface ITcpServer
{
    Task SendResponseAsync(ResponseBase request);
    Task ConnectAsync(IConfig config);
    event Func<RequestBase, Task> Received;
}
