using StreamingApp.BLL.Models;
using StreamingApp.WPF.ViewModels.Base;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;

namespace StreamingApp.WPF.ViewModels.ControlsViewModels;

internal class UserPanelViewModel : ViewModelBase
{
    private readonly ImageSourceConverter _converter;
    private string _username;
    private string _avatarIcon;
    private bool _isVideoOn;

    public string Username => _username;
    public ImageSource? AvatarIcon
    {
        get
        {
            if (_avatarIcon == "0")
            {
                return new BitmapImage(new Uri("Images/user.JPG", UriKind.Relative));
            }
            else
            {
                var buffer = Convert.FromBase64String(_avatarIcon);
                return (ImageSource?)_converter.ConvertFrom(buffer);
            }
        }
    }
    public int UserId { get; set; }
    public bool IsVideoOn
    {
        get => _isVideoOn;
        set
        {
            _isVideoOn = value;
            OnPropertyChanged();
        }
    }

    public UserPanelViewModel(User user, bool isSharing = false)
    {
        _converter = new ImageSourceConverter();
        _username = user.Name;
        _avatarIcon = user.AvatarImage;
        _isVideoOn = isSharing;
        UserId = user.Id;
    }
}
