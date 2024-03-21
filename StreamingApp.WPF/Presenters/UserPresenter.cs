using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Responses;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels;
using StreamingApp.WPF.ViewModels.Base;
using StreamingApp.WPF.ViewModels.ControlsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace StreamingApp.WPF.Presenters;

internal class UserPresenter : IUserPresenter
{
    private readonly ChatNavigationStore _navigationStore;

    public UserPresenter(ChatNavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void ChangeView(ResponseBase response)
    {
        _navigationStore.CurrentViewModel = response switch
        {
            ErrorResponse => new ErrorViewModel(),
            LoginResponse loginResponse => OnLogin(loginResponse),
            ConnectResponse connectResponse => OnConnect(connectResponse),
            CreateMeetingResponse createResponse => OnCreate(createResponse),
            _ => _navigationStore.CurrentViewModel,
        };

        _navigationStore.UsersViewModel = response switch
        {
            CreateMeetingResponse createResponse => OnUserConnected(createResponse),
            ConnectResponse connectResponse => OnUserConnected(connectResponse),
            StartSharingResponse shareResponse => OnUserShare(
                _navigationStore.UsersViewModel as UsersListViewModel, shareResponse.SenderId),
            _ => _navigationStore.UsersViewModel,
        };
    }

    private ViewModelBase? OnUserShare(UsersListViewModel viewModel, int id)
    {
        viewModel.Users.First(u => u.UserId == id).IsVideoOn = true;

        return viewModel;
    }

    private ViewModelBase? OnUserConnected(ConnectResponse connectResponse)
    {
        return UserConnected(connectResponse.Meeting.Users);
    }

    private ViewModelBase? OnUserConnected(CreateMeetingResponse connectResponse)
    {
        return UserConnected(connectResponse.Meeting.Users);
    }

    private ViewModelBase? UserConnected(ICollection<User> users)
    {
        return new UsersListViewModel(
            users.Select(u => new UserPanelViewModel(u)).ToList());
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
