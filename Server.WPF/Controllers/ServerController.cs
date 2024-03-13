using Networking;
using Server.WPF.ViewModels;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.UseCase;
using StreamingApp.Networking.Configs;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Server.WPF.Controllers;

public class ServerController
{
    private readonly UseCaseInteractor _useCaseInteractor;
    public ServerController(UseCaseInteractor useCaseInteractor)
    {
        _useCaseInteractor = useCaseInteractor;
        _useCaseInteractor.UserCountChanged += _useCaseInteractor_UserCountChanged;
    }

    public ViewModelBase? ViewModel { get; set; }
    public int UsersCount { get; set; }

    private void _useCaseInteractor_UserCountChanged(int count)
    {
        UsersCount = count;
        ViewModel?.OnPropertyChanged("ConnectedUsersCount");
    }

    public async Task ConnectServerAsync()
    {
        var config = NetworkConfiguration.GetStaticConfig();
        await _useCaseInteractor.ConnectAsync(config);
    }
}
