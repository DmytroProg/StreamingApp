using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;

namespace StreamingApp.BLL.Interfaces;

public interface ITcpClient
{
    Task<ResponseBase> SendRequestAsync(RequestBase request);
    Task ConnectAsync(IConfig config);
    event Action<ResponseBase> Received;
}
