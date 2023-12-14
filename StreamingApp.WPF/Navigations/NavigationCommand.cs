using System.Windows.Input;
using System;

namespace StreamingApp.WPF.Navigations;

internal class NavigationCommand : ICommand
{
    private readonly NavigationService _navigationService;

    public event EventHandler? CanExecuteChanged;

    public NavigationCommand(NavigationService navigationService)
    {
        this._navigationService = navigationService;
    }

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        this._navigationService.Navigate();
    }
}