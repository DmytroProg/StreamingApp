using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Responses;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels;

namespace StreamingApp.WPF.Presenters;

internal class UserPresenter : IPresenter
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
            LoginResponse loginResponse => new ConnectViewModel(),
            ConnectResponse connectResponse => new TestViewModel(connectResponse.Meeting.Title),
            _ => new ErrorViewModel()
        };
    }
}
