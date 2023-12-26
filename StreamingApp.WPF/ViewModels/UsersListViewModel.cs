using StreamingApp.BLL.Models;
using StreamingApp.WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace StreamingApp.WPF.ViewModels;

class UsersListViewModel : ViewModelBase
{
    private ObservableCollection<object> users;

    public ObservableCollection<object> Users
    {
        get { return users; }
        set
        {
            if (users != value)
            {
                users = value;
                OnPropertyChanged();
            }
        }
    }

    public UsersListViewModel()
    {
        Users = new ObservableCollection<object>();

        AddUser("/Images/user.JPG", "Dmitro", "/Images/se4.png", "/Images/br1.png");
        AddUser("/Images/user.JPG", "Vitalic", "/Images/se4.png", "/Images/br1.png");
    }

    public void AddUser(string avatarPath, string userName, string chatIconPath, string videoIconPath)
    {
        Users.Add(new
        {
            AvatarPath = avatarPath,
            UserName = userName,
            ChatIconPath = chatIconPath,
            VideoIconPath = videoIconPath
        });
    }
}
