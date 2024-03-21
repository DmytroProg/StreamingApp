using GalaSoft.MvvmLight.Command;
using StreamingApp.BLL.Models;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels.Base;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace StreamingApp.WPF.ViewModels;

internal class RegistrationViewModel : ViewModelBase
{
    private string _nickname;
    private string _login;
    private string _password;

    public string Nickname
    {
        get => _nickname;
        set
        {
            if (value.Length >= 15)
            {
                MessageBox.Show("Nickname should be more than 15 characters long");
                return;
            }
            _nickname = value;
            OnPropertyChanged();
        }
    }

    public string Login
    {
        get => _login;
        set
        {
            if (value.Length >= 15)
            {
                MessageBox.Show("Login should be more than 15 characters long");
                return;
            }
            _login = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (value.Length <= 5)
            {
                MessageBox.Show("The password must contain at least 6 characters");
                return;
            }
            _password = value;
            OnPropertyChanged();
        }
    }

    public string Avatar { get; set; }

    public ICommand RegisterCommand { get; }
    public ICommand SelectAvatarCommand { get; }

    public RegistrationViewModel() {
        RegisterCommand = new RelayCommand(Register);
        SelectAvatarCommand = new RelayCommand(SelectAvatar);
    }

    private void SelectAvatar()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF";
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            var stream = openFileDialog.OpenFile();
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            Avatar = Convert.ToBase64String(ms.ToArray());
        }
    }

    public void Register()
    {
        var user = new User() { 
            Name = Nickname,
            Login = Login,
            Password = Password,
            AvatarImage = Avatar ?? "0"
        };
        App.UnitController.UserController.Register(user);
    }
}
