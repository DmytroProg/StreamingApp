using Microsoft.Extensions.DependencyInjection;
using Networking.Network;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.UseCase;
using StreamingApp.Networking.Network;
using System.Runtime.Versioning;

namespace CompositionRoot;

public static class HostExtention
{
    public static void AddServerServices(this IServiceCollection services)
    {
        services.AddDefaultServices();
        services.AddSingleton<UseCaseInteractor>();
    }

    public static void AddDefaultServices(this IServiceCollection services)
    {
        services.AddSingleton<ITcpClient, TcpClientUser>();
        services.AddSingleton<ITcpServer, TcpClientServer>();
    }

    [SupportedOSPlatform("windows")]
    public static void AddWindowsServices(this IServiceCollection services)
    {
        services.AddTransient<ILogger, StreamingAppLogger>();
    }
}
