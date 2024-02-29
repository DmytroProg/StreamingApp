using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;

namespace StreamingApp.BLL.Interfaces;

    public interface ITcpServer
    {
        Task StartListenAsync(IConfig config);
        Task SendResponseAsync(TcpClient client, ResponseBase response);
        event Action<RequestBase, TcpClient>? RequestReceived;
    }
}
