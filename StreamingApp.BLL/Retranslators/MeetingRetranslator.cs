using Server.DAL.Models;
using StreamingApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Retranslators
{
    public class MeetingRetranslator
    {
        private readonly UserRetranslator _userRetranslator;
        private readonly MessageRetranslator _messageRetranslator;

        public MeetingRetranslator()
        {
            _userRetranslator = new UserRetranslator();
            _messageRetranslator = new MessageRetranslator();
        }

        public Meeting TranslateMeetingInfoToMeeting(MeetingInfo meetingInfo)
        {
            Meeting meeting = new Meeting()
            {
                Id = meetingInfo.Id,
                Title = meetingInfo.Title,
                UserId = meetingInfo.UserId,
                Users = meetingInfo.Users.Select(u => _userRetranslator.TranslateUserInfoToUser(u)).ToList(),
                Admin = _userRetranslator.TranslateUserInfoToUser(meetingInfo.Admin),
                Messages = meetingInfo.Messages.Select(m => _messageRetranslator.TranslateMessageBaseInfoToMessageBase(m)).ToList()
            };
            return meeting;
        }

        public MeetingInfo TranslateMeetingToMeetingInfo(Meeting meeting)
        {
            MeetingInfo meetingInfo = new MeetingInfo()
            {
                Id = meeting.Id,
                Title = meeting.Title,
                UserId = meeting.UserId,
                Users = meeting.Users.Select(u => _userRetranslator.TranslateUserToUserInfo(u)).ToList(),
                Admin = _userRetranslator.TranslateUserToUserInfo(meeting.Admin),
                Messages = meeting.Messages.Select(m => _messageRetranslator.TranslateMessageBaseToMessageBaseInfo(m)).ToList()
            };
            return meetingInfo;
        }
    }
}
