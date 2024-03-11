using StreamingApp.WPF.Controllers;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.Presenters;
using StreamingApp.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using CompositionRoot;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.WPF.Controllers.Base;

namespace StreamingApp.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly NavigationStore _navigationStore;

    public static UnitController UnitController { get; private set; }
    public App()
    {
        using var host = CreateHostBuilder().Build();
        _navigationStore = host.Services.GetRequiredService<NavigationStore>();
        _navigationStore.CurrectViewModel = new LoginViewModel(_navigationStore);

        UnitController = new UnitController(host);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore),
        };
        MainWindow.Show();
    }

    private IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddDefaultServices();
                services.AddWindowsServices();

                services.AddSingleton<NavigationStore>();
                services.AddSingleton<IUserPresenter, UserPresenter>();
                services.AddSingleton<IMessagePresenter, MessagePresenter>();
                services.AddSingleton<IScreenSharePresenter, ScreenSharePresenter>();

                services.AddSingleton<UserController>();
                services.AddSingleton<MessageController>();
                services.AddSingleton<ScreenShareController>();
            });
    }
}
