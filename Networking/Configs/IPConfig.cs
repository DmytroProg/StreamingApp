using StreamingApp.BLL.Interfaces;
using System.Net;

namespace StreamingApp.Networking.Configs;

public class IPConfig : IConfig
{
    public IPAddress IPAddress { get; set; } = null!;
    public int Port { get; set; }
}
