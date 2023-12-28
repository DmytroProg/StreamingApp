using StreamingApp.WPF.ViewModels.Base;

namespace StreamingApp.WPF.ViewModels.ControlsViewModels;

internal class UserPanelViewModel : ViewModelBase
{
    private string _username;
    private string _avatarIcon;
    private bool _isVideoOn;

    public string Username => _username;
    public string AvatarIcon => _avatarIcon;
    public bool IsVideoOn
    {
        get => _isVideoOn;
        set
        {
            _isVideoOn = value;
            OnPropertyChanged();
        }
    }

    public UserPanelViewModel(string username, string avatar)
    {
        _username = username;
        _avatarIcon = avatar;
    }
}
