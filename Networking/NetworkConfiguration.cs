﻿using StreamingApp.Networking.Configs;
using System.Net;

namespace Networking;

public static class NetworkConfiguration
{
    public static int Port = 8888;
    public static IPConfig GetStaticConfig(int? port = null)
    {
        var hostName = Dns.GetHostName();
        var hostAddress = Dns.GetHostAddresses(hostName);
        var ipAddress = hostAddress.FirstOrDefault(ip => ip.AddressFamily
        == System.Net.Sockets.AddressFamily.InterNetwork) ??
        throw new ArgumentNullException("IpAddress", "Cannot get available IP address");

        return new IPConfig()
        {
            IPAddress = ipAddress,
            Port = port ?? NetworkConfiguration.Port,
        };
    }
}