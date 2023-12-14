using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels.Base;

namespace StreamingApp.WPF.ViewModels;

internal class MainViewModel : ViewModelBase
{
    private NavigationStore _navigationStore;

    public ViewModelBase? CurrectViewModel { get => _navigationStore.CurrectViewModel; }

    public MainViewModel(NavigationStore navigationStore)
    {
        this._navigationStore = navigationStore;

        this._navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrectViewModel));
    }
}