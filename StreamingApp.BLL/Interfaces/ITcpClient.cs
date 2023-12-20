using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using System.Net;
using System.Net.Sockets;

namespace StreamingApp.BLL.Interfaces;

public interface ITcpClient
{
    void Send(RequestBase request, TcpClient client);
    void Connect(IPAddress iPAddress, int port);

    event Action<ResponseBase> Received; 
}
