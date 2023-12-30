using Microsoft.Extensions.DependencyInjection;
using StreamingApp.BLL.Interfaces;
using StreamingApp.Networking.Network;
using System.Runtime.Versioning;

namespace CompositionRoot;

public static class HostExtention
{
    public static void AddDefaultServices(this IServiceCollection services)
    {
        services.AddSingleton<ITcpClient, TcpClientUser>();
    }

    [SupportedOSPlatform("windows")]
    public static void AddWindowsServices(this IServiceCollection services)
    {
        services.AddTransient<ILogger, StreamingAppLogger>();
    }
}
