using StreamingApp.WPF.ViewModels.Base;
using System;

namespace StreamingApp.WPF.Navigations;

internal class NavigationStore
{
    private ViewModelBase? _currentViewModel;

    public ViewModelBase? CurrectViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    public event Action? CurrentViewModelChanged;

    private void OnCurrentViewModelChanged()
    {
        this.CurrentViewModelChanged?.Invoke();
    }
}
