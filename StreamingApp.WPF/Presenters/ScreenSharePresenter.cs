using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Responses;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels;
using System.Windows.Controls;
using System.Windows.Media;

namespace StreamingApp.WPF.Presenters;

internal class ScreenSharePresenter : IScreenSharePresenter
{
    private readonly NavigationStore _navigationStore;
    private readonly IUdpClient _udpClient;

    public ScreenSharePresenter(NavigationStore navigationStore, IUdpClient udpClient)
    {
        _navigationStore = navigationStore;
        _udpClient = udpClient;
    }

    public void ChangeView(ResponseBase response)
    {
        if (response is StartSharingResponse frameRes)
        {
            if(frameRes.SenderId == UserInfo.CurrentUser.Id)
            {
                App.UnitController.ScreenShareController.SendScreenAsync();
            }
            else
            {
                _navigationStore.CurrectViewModel = new ScreenShareViewModel(_udpClient);
            }
        }
    }
}
