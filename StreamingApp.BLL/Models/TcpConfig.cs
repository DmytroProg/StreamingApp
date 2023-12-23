using StreamingApp.BLL.Interfaces;
using System.Net;

namespace StreamingApp.BLL.Models;

public class TcpConfig : IConfig
{
    public IPAddress IPAddress { get; set; } = null!;
    public int Port { get; set; }
}
