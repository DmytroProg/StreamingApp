using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Services
{
    public class MeetingServise : IService<Meeting>
    {
        private IMeetingRepository _meetingRepository;

        public MeetingServise(IMeetingRepository repository)
        {
            _meetingRepository = repository;
        }

        public async Task AddAsync(Meeting obj)
        {
            await _meetingRepository.AddObjectAsync(obj);
        }

        public async Task<Meeting> GetByIdAsync(int id)
        {
            var meeting = await _meetingRepository.GetObjectByIdAsync(id);
            if (meeting != null)
                return meeting;
            return null;
        }

        public Task<IEnumerable<Meeting>> QueryMany(Predicate<Meeting> query)
        {
            throw new NotImplementedException();
        }

        public Task<Meeting> QueryOne(Predicate<Meeting> query)
        {
            throw new NotImplementedException();
        }
    }
}
