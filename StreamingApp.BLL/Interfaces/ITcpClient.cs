using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using System.Net;
using System.Net.Sockets;

namespace StreamingApp.BLL.Interfaces;

public interface ITcpClient
{
    Task<ResponseBase> SendRequestAsync(RequestBase request);
    Task ConnectAsync(IConfig config);
    Task SendFileAsync(LoadFileRequest fileRequest, byte[] fileData);
    event Action<ResponseBase> Received;
}
