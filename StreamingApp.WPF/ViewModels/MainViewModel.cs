using GalaSoft.MvvmLight.Command;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels.Base;
using StreamingApp.WPF.ViewModels.ControlsViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StreamingApp.WPF.ViewModels;

internal class MainViewModel : ViewModelBase
{
    private DrawWindow _drawWindow;

    private readonly ImageSourceConverter _converter;
    private ChatNavigationStore _navigationStore;
    private bool _isActive;
    private bool _isActiveLogo;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            OnPropertyChanged();
        }
    }

    public bool IsActiveLogo
    {
        get => _isActiveLogo;
        set
        {
            _isActiveLogo = value;
            OnPropertyChanged();
        }
    }

    public ImageSource? Avatar {
        get
        {
            if (UserInfo.CurrentUser is null || UserInfo.CurrentUser.AvatarImage == "0")
            {
                return new BitmapImage(new Uri("Images/user.JPG", UriKind.Relative));
            }
            else
            {
                var buffer = Convert.FromBase64String(UserInfo.CurrentUser.AvatarImage);
                return (ImageSource?)_converter.ConvertFrom(buffer);
            }
        }    
    }

    public string UserName => UserInfo.CurrentUser is null ? "User" : UserInfo.CurrentUser.Name;

    public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;
    public ViewModelBase? UsersListViewModel => _navigationStore.UsersViewModel;
    public ViewModelBase? ChatViewModel => _navigationStore.ChatViewModel;

    public ICommand StartSharingCommand { get; }

    public bool IsChatViewModelCurrent => CurrentViewModel is ChatViewModel;

    public MainViewModel(ChatNavigationStore navigationStore)
    {
        _converter = new ImageSourceConverter();
        this._navigationStore = navigationStore;
        this._navigationStore.ChatViewModel = new LoginActionViewModel();
        this._navigationStore.UsersViewModel = new LoginActionViewModel();
        this._navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        this._navigationStore.ChatViewModelChanged += OnChatViewModelChanged;
        this._navigationStore.UsersViewModelChanged += OnUsersViewModelChanged;
        
        IsActive = false;
        IsActiveLogo = true;

        StartSharingCommand = new RelayCommand(StartSharing);
    }

    private void OnUsersViewModelChanged()
    {
        OnPropertyChanged(nameof(UsersListViewModel));
    }

    private void OnChatViewModelChanged()
    {
        OnPropertyChanged(nameof(ChatViewModel));
    }

    private void StartSharing()
    {
        App.UnitController.ScreenShareController.StartSharing();
        _drawWindow = new DrawWindow();
        _drawWindow.Show();
    }

    private void OnCurrentViewModelChanged()
    {
        CheckCurrentViewModel();
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void CheckCurrentViewModel()
    {
        Action? changeView = CurrentViewModel switch
        {
            //ViewModels.ChatViewModel => OnChangeMeetingView,
            //MeetingViewModelBase => OnChangeMeetingView,
            ConnectViewModel => delegate() {
                OnPropertyChanged(nameof(Avatar));
                OnPropertyChanged(nameof(UserName));
            },
            _ => null,
        };

        IsActive = CurrentViewModel is MeetingViewModelBase;
        IsActiveLogo = !IsActive;

        changeView?.Invoke();
    }

    private void OnChangeMeetingView()
    {
        _navigationStore.ChatViewModel = new ChatViewModel(UserInfo.Meeting);
        _navigationStore.UsersViewModel = new UsersListViewModel(new List<UserPanelViewModel>());
        OnPropertyChanged(nameof(ChatViewModel));
        OnPropertyChanged(nameof(UsersListViewModel));
    }

    ~MainViewModel()
    {
        _drawWindow.Close();
        _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
        _navigationStore.UsersViewModelChanged -= OnUsersViewModelChanged;
        _navigationStore.ChatViewModelChanged -= OnChatViewModelChanged;
    }
}