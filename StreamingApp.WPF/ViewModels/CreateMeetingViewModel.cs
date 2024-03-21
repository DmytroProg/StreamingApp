using GalaSoft.MvvmLight.Command;
using StreamingApp.BLL.Models;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                if (value.Length > 40)
                {
                    MessageBox.Show("The name must be less than 40 characters long");
                    return;
                }
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
        public ICommand GenerateCodeCommand { get; }

        public CreateMeetingViewModel()
        {
            GenerateCode();
            CreateMeetingCommand = new RelayCommand(CreateMeeting);
            GenerateCodeCommand = new RelayCommand(GenerateCode);
        }

        private void CreateMeeting()
        {
            var meeting = new Meeting()
            {
                MeetingCode = this.MeetingCode,
                AdminId = UserInfo.CurrentUser.Id,
                Title = this.MeetingName,
                Messages = new List<MessageBase>(),
                Users = new List<User>()
            };
            App.UnitController.UserController.CreateMeeting(meeting);
        }

        private void GenerateCode()
        {
            MeetingCode = Guid.NewGuid()
                .ToString()
                .Substring(0, 6);
        }
    }
}
