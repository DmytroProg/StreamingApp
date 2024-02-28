using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Interfaces
{
    public interface ITcpServer
    {
        Task StartListenAsync(IConfig config);
        Task SendResponseAsync(TcpClient client, ResponseBase response);
        event Action<RequestBase, TcpClient>? RequestReceived;
    }
}
