using StreamingApp.WPF.ViewModels.Base;
using System;

namespace StreamingApp.WPF.Navigations;

internal class ChatNavigationStore : NavigationStore
{
    private ViewModelBase? _chatViewModel;

    public ViewModelBase? ChatViewModel
    {
        get => _chatViewModel;
        set
        {
            _chatViewModel = value;
            OnChatViewModelChanged();
        }
    }

    public event Action? ChatViewModelChanged;

    private void OnChatViewModelChanged()
    {
        this.ChatViewModelChanged?.Invoke();
    }
}
