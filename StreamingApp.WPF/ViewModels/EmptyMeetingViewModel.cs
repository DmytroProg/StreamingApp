using StreamingApp.BLL.Models;
using StreamingApp.WPF.ViewModels.Base;

namespace StreamingApp.WPF.ViewModels;

internal class EmptyMeetingViewModel : ViewModelBase
{
    public Meeting Meeting { get; set; }

    public EmptyMeetingViewModel(Meeting meeting)
    {
        Meeting = meeting;
    }
}
