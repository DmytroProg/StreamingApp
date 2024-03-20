using StreamingApp.BLL.Models;
using StreamingApp.WPF.ViewModels.Base;

namespace StreamingApp.WPF.ViewModels;

internal class EmptyMeetingViewModel : MeetingViewModelBase
{

    public EmptyMeetingViewModel(Meeting meeting) : base(meeting)
    {
        Meeting = meeting;
    }
}
