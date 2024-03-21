using StreamingApp.WPF.ViewModels.Base;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace StreamingApp.WPF.ViewModels.ControlsViewModels;

class UsersListViewModel : ViewModelBase
{
    public ObservableCollection<UserPanelViewModel> Users { get; set; }

    public UsersListViewModel(ICollection<UserPanelViewModel> users)
    {
        Users = new ObservableCollection<UserPanelViewModel>(users);
        OnPropertyChanged(nameof(Users));
    }
}
