using StreamingApp.WPF.ViewModels.Base;
using System;

namespace StreamingApp.WPF.Navigations;

internal class NavigationService
{
    private readonly NavigationStore _navigationStore;
    private Func<ViewModelBase> _createViewModel;

    public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
    {
        this._navigationStore = navigationStore;
        this._createViewModel = createViewModel;
    }

    public void Navigate()
    {
        this._navigationStore.CurrentViewModel = _createViewModel();
    }
}
