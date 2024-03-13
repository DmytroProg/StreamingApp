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

    public UserPresenter(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void ChangeView(ResponseBase response)
    {
        _navigationStore.CurrectViewModel = response switch
        {
            LoginResponse loginResponse => OnLogin(loginResponse),
            ConnectResponse connectResponse => OnConnect(connectResponse),
            CreateMeetingResponse createResponse => OnCreate(createResponse),
            _ => new ErrorViewModel()
        };
    }

    private ViewModelBase OnCreate(CreateMeetingResponse createResponse)
    {
        UserInfo.MeetingId = createResponse.Meeting.Id;
        return new TestViewModel();
    }

    private ViewModelBase OnConnect(ConnectResponse connectResponse)
    {
        UserInfo.MeetingId = connectResponse.Meeting.Id;
        return new TestViewModel();
    }

    private ViewModelBase OnLogin(LoginResponse response)
    {
        UserInfo.CurrentUser = response.User;
        return new ConnectViewModel(_navigationStore);
    }
}
