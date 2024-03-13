using GalaSoft.MvvmLight.Command;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels.Base;
using StreamingApp.WPF.ViewModels.ControlsViewModels;
using System;
using System.Windows.Input;

namespace StreamingApp.WPF.ViewModels;

internal class MainViewModel : ViewModelBase
{
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

    public ViewModelBase? CurrectViewModel { get => _navigationStore.CurrectViewModel; }
    public ViewModelBase UsersListViewModel { get; set; }
    public ViewModelBase ChatViewModel { get; set; }

    public ICommand StartSharingCommand { get; }

    public bool IsChatViewModelCurrent => CurrectViewModel is ChatViewModel;

    public MainViewModel(NavigationStore navigationStore)
    {
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
        OnPropertyChanged(nameof(CurrectViewModel));
    }

    private void CheckCurrentViewModel()
    {
        if(CurrectViewModel is ChatViewModel)
        {
            ChatViewModel = new ChatViewModel();
            UsersListViewModel = new UsersListViewModel();
            OnPropertyChanged(nameof(ChatViewModel));
            OnPropertyChanged(nameof(UsersListViewModel));
        }
        if (!(CurrectViewModel is RegistrationViewModel) && !(CurrectViewModel is LoginViewModel))
        {
            IsActive = true;
            IsActiveLogo = false;
        }
        else 
        {
            IsActive = false;
            IsActiveLogo = true;
        }
    }
}