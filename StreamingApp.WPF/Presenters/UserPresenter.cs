using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Responses;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels;
using StreamingApp.WPF.ViewModels.Base;
using System;

namespace StreamingApp.WPF.Presenters;

internal class UserPresenter : IUserPresenter
{
    private readonly NavigationStore _navigationStore;

    public UserPresenter(ChatNavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void ChangeView(ResponseBase response)
    {
        _navigationStore.CurrentViewModel = response switch
        {
            LoginResponse loginResponse => OnLogin(loginResponse),
            ConnectResponse connectResponse => OnConnect(connectResponse),
            CreateMeetingResponse createResponse => OnCreate(createResponse),
            _ => new ErrorViewModel()
        };
    }

    private ViewModelBase OnCreate(CreateMeetingResponse createResponse)
    {
        UserInfo.Meeting = createResponse.Meeting;
        return new EmptyMeetingViewModel(createResponse.Meeting);
    }

    private ViewModelBase OnConnect(ConnectResponse connectResponse)
    {
        UserInfo.Meeting = connectResponse.Meeting;
        return new EmptyMeetingViewModel(connectResponse.Meeting);
    }

    private ViewModelBase OnLogin(LoginResponse response)
    {
        UserInfo.CurrentUser = response.User;
        return new ConnectViewModel(_navigationStore);
    }
}
