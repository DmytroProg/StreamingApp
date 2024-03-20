using StreamingApp.BLL.Models;

namespace StreamingApp.WPF.ViewModels.Base;

internal class MeetingViewModelBase : ViewModelBase
{
    public MeetingViewModelBase(Meeting meeting)
    {
        Meeting = meeting;
    }
    public Meeting Meeting { get; set; }
}
