using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Networking.Network;
using Server.DAL.Entities;
using Server.DAL.Implementations;
using Server.DAL.Repositories;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
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
        services.AddSingleton<ITcpServer,  TcpClientServer>();
    }

    public static void AddDbRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITextMessageRepository, TextMessageRepository>();
        services.AddTransient<IMeetingRepository, MeetingRepository>();
    }

    [SupportedOSPlatform("windows")]
    public static void AddWindowsServices(this IServiceCollection services)
    {
        services.AddTransient<ILogger, StreamingAppLogger>();
    }
}
