using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Server.WPF.ViewModels;

internal class MainViewModel : ViewModelBase
{
    private string? _ipAddress;
    private string _port;
    private bool _isSettingsPopupOpen;

    public bool IsSettingsPopupOpen
    {
        get => _isSettingsPopupOpen;
        set
        {
            _isSettingsPopupOpen = value;
            OnPropertyChanged();
        }
    }

    public string? IpAddress
    {
        get => _ipAddress;
        set
        {
            _ipAddress = value;
            OnPropertyChanged();
        }
    }

    public string Port
    {
        get => _port;
        set
        {
            _port = value;
            OnPropertyChanged();
        }
    }
    public ICommand ListenCommand { get; }
    public ICommand SettingsCommand { get; }
    public ICommand ClosePopupCommand { get; }

    public MainViewModel()
    {
        ListenCommand = new RelayCommand(Connect);
        SettingsCommand = new RelayCommand(Settings);
        ClosePopupCommand = new RelayCommand(ClosePopup);
    }

    private void Settings()
    {
        try
        {
            IsSettingsPopupOpen = true;
            //MessageBox.Show($"IsSettingsPopupOpen: {IsSettingsPopupOpen}");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Connect()
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ClosePopup()
    {
        IsSettingsPopupOpen = false;
    }
}
