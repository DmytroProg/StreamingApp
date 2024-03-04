using CompositionRoot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.WPF.Controllers;
using Server.WPF.ViewModels;
using System.Windows;

namespace Server.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static ServerController ServerController { get; private set; } = null!;

    public App()
    {
        using var host = CreateHostBuilder().Build();

        ServerController = host.Services.GetRequiredService<ServerController>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(),
        };
        MainWindow.Show();
    }

    private IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddWindowsServices();
                services.AddServerServices();

                services.AddSingleton<ServerController>();
            });
    }
}
