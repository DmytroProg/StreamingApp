using StreamingApp.WPF.ViewModels.Base;
using System;

namespace StreamingApp.WPF.Navigations;

internal class ChatNavigationStore : NavigationStore
{
    private ViewModelBase? _chatViewModel;
    private ViewModelBase? _usersViewModel;

    public ViewModelBase? ChatViewModel
    {
        get => _chatViewModel;
        set
        {
            _chatViewModel = value;
            OnChatViewModelChanged();
        }
    }

    public ViewModelBase? UsersViewModel
    {
        get => _usersViewModel;
        set
        {
            _usersViewModel = value;
            OnChatViewModelChanged();
        }
    }

    public event Action? ChatViewModelChanged;
    public event Action? UsersViewModelChanged;

    private void OnChatViewModelChanged()
    {
        this.ChatViewModelChanged?.Invoke();
    }

    private void OnUsersViewModelChanged()
    {
        this.UsersViewModelChanged?.Invoke();
    }
}
