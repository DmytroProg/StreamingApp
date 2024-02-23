using Server.DAL.Repositories;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Retranslators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Services
{
    public class MeetingServise : IServise<Meeting>
    {
        private MeetingRepository _meetingRepository;
        private MeetingRetranslator _meetingRetranslator;

        public MeetingServise()
        {
            _meetingRepository = new MeetingRepository();
            _meetingRetranslator = new MeetingRetranslator();
        }

        public async Task AddAsync(Meeting obj)
        {
            await _meetingRepository.AddObjectAsync(_meetingRetranslator.TranslateMeetingToMeetingInfo(obj));
        }

        public async Task<Meeting> GetByIdAsync(int id)
        {
            var meeting = await _meetingRepository.GetObjectByIdAsync(id);
            if (meeting != null)
                return _meetingRetranslator.TranslateMeetingInfoToMeeting(meeting);
            return null;
        }
    }
}
