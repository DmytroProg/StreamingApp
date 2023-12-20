using StreamingApp.BLL.Controllers;
using StreamingApp.BLL.Interfaces;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.Presenters;
using StreamingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StreamingApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly IPresenter _usersPresenter;

        public App()
        {
            _navigationStore = new NavigationStore();
            _usersPresenter = new UsersPresenter(_navigationStore);

            var controller = new UsersController(null, null, _usersPresenter);
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
