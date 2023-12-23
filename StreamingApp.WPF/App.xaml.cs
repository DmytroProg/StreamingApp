using StreamingApp.BLL.Controllers;
using StreamingApp.BLL.Interfaces;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.Presenters;
using StreamingApp.WPF.ViewModels;
using System.Windows;

namespace StreamingApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserController UserController { get; private set; } = null!;

        private readonly NavigationStore _navigationStore;
        private readonly IPresenter _usersPresenter;

        public App()
        {
            _navigationStore = new NavigationStore();
            _navigationStore.CurrectViewModel = new LoginViewModel();
            _usersPresenter = new UserPresenter(_navigationStore);

            UserController = new UserController(null, null, _usersPresenter);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore),
            };
            MainWindow.Show();
        }
    }
}
