using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Responses;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels;

namespace StreamingApp.WPF.Presenters;

internal class UserPresenter : IUserPresenter
{
    private readonly NavigationStore _navigationStore;

    public UserPresenter(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void ChangeView(ResponseBase response)
    {
        _navigationStore.CurrectViewModel = response switch
        {
            LoginResponse loginResponse => new ConnectViewModel(_navigationStore),
            //ConnectResponse connectResponse => new ChatViewModel(),
            ConnectResponse connectResponse => new ScreenShareViewModel(),
            _ => new ErrorViewModel()
        };
    }
}
