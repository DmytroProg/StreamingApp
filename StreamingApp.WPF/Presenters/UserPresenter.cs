using StreamingApp.BLL.Interfaces;
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
            //ConnectResponse connectResponse => new ChatViewModel(),
            ConnectResponse connectResponse => new ScreenShareViewModel(),
            CreateMeetingResponse createResponse => OnCreate(createResponse),
            _ => new ErrorViewModel()
        };
    }

    private CreateMeetingViewModel OnCreate(CreateMeetingResponse createResponse)
    {
        UserInfo.MeetingId = createResponse.Meeting.Id;
        return new CreateMeetingViewModel();
    }

    private ViewModelBase OnLogin(LoginResponse response)
    {
        UserInfo.CurrentUser = response.User;
        return new ConnectViewModel(_navigationStore);
    }
}
