using GalaSoft.MvvmLight.Command;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels.Base;
using StreamingApp.WPF.ViewModels.ControlsViewModels;
using System;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StreamingApp.WPF.ViewModels;

internal class MainViewModel : ViewModelBase
{
    private readonly ImageSourceConverter _converter;
    private NavigationStore _navigationStore;
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

    public ViewModelBase? CurrentViewModel { get => _navigationStore.CurrectViewModel; }
    public ViewModelBase UsersListViewModel { get; set; }
    public ViewModelBase ChatViewModel { get; set; }

    public ICommand StartSharingCommand { get; }

    public bool IsChatViewModelCurrent => CurrentViewModel is ChatViewModel;

    public MainViewModel(NavigationStore navigationStore)
    {
        _converter = new ImageSourceConverter();
        this._navigationStore = navigationStore;
        this._navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

        UsersListViewModel = ChatViewModel = new LoginActionViewModel();
        IsActive = false;
        IsActiveLogo = true;

        StartSharingCommand = new RelayCommand(StartSharing);
    }

    private void StartSharing()
    {
        App.UnitController.ScreenShareController.StartSharing();
    }

    private void OnCurrentViewModelChanged()
    {
        CheckCurrentViewModel();
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void CheckCurrentViewModel()
    {
        if(CurrentViewModel is ChatViewModel)
        {
            ChatViewModel = new ChatViewModel();
            UsersListViewModel = new UsersListViewModel();
            OnPropertyChanged(nameof(ChatViewModel));
            OnPropertyChanged(nameof(UsersListViewModel));
        }
        if(CurrentViewModel is ConnectViewModel)
        {
            OnPropertyChanged(nameof(Avatar));
            OnPropertyChanged(nameof(UserName));
        }

        IsActive = UserInfo.MeetingId != 0;
        IsActiveLogo = !IsActive;
    }
}