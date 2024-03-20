using StreamingApp.WPF.ViewModels.Base;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StreamingApp.WPF.ViewModels.ControlsViewModels;

class UsersListViewModel : ViewModelBase
{
    public ObservableCollection<UserPanelViewModel> Users { get; set; }

    public UsersListViewModel(ICollection<UserPanelViewModel> users)
    {
        Users = new ObservableCollection<UserPanelViewModel>(users);

        //Users.Add(new UserPanelViewModel("Dmitro", "/Images/user.JPG"));
        //Users.Add(new UserPanelViewModel("Vitalic", "/Images/user.JPG") { IsVideoOn = true});
    }
}
