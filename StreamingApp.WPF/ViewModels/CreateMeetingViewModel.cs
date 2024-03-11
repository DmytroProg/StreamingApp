using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StreamingApp.WPF.ViewModels
{
    class CreateMeetingViewModel : ViewModelBase
    {
        private string _meetingName;
        private string _meetingCode;

        public string MeetingName
        {
            get => _meetingName;
            set
            {
                _meetingName = value;
                OnPropertyChanged();
            }
        }

        public string MeetingCode
        {
            get => _meetingCode;
            set
            {
                _meetingCode = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateMeetingCommand { get; }

        public CreateMeetingViewModel()
        {
            MeetingCode = "_______";
        }
    }
}
