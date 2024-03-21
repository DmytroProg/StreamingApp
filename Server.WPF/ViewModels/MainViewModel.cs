using GalaSoft.MvvmLight.Command;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Server.WPF.ViewModels;

internal class MainViewModel : ViewModelBase
{
    private string? _ipAddress;
    private string _port;
    private bool _isSettingsPopupOpen;
    private string _statusText;
    private Brush _statusTextColor;
    private int _connectedUsersCount;

    public ObservableCollection<User> Users { get; set; }

    public int ConnectedUsersCount => App.ServerController.UsersCount;

    public string StatusText
    {
        get => _statusText;
        set
        {
            _statusText = value;
            OnPropertyChanged();
        }
    }

    public Brush StatusTextColor
    {
        get => _statusTextColor;
        set
        {
            _statusTextColor = value;
            OnPropertyChanged();
        }
    }

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
            if (value != null && IPAddress.TryParse(value, out IPAddress _))
            {
                _ipAddress = value;
                OnPropertyChanged();
            }
            else
            {
                MessageBox.Show("Invalid IP address");
            }
        }
    }

    public string Port
    {
        get => _port;
        set
        {
            if (int.TryParse(value, out int port) && port >= 1024 && port <= 65535)
            {
                _port = value;
                OnPropertyChanged();
            }
            else
            {
                MessageBox.Show("Invalid port number");
            }
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
        Users = new ObservableCollection<User>();
        StatusTextColor = Brushes.Red;
        StatusText = "Waiting for connection...";
        App.ServerController.ViewModel = this;
    }

    private void Settings()
    {
        try
        {
            IsSettingsPopupOpen = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async void Connect()
    {
        try
        {
            StatusText = "Connected...";
            StatusTextColor = Brushes.Green;
            await App.ServerController.ConnectServerAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            StatusTextColor = Brushes.Red;
        }
    }

    private void ClosePopup()
    {
        IsSettingsPopupOpen = false;
    }
}
