using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Services
{
    public class MeetingServise : IService<Meeting>
    {
        private IMeetingRepository _meetingRepository;
        private readonly ILogger _logger;

        public MeetingServise(IMeetingRepository repository, ILogger logger)
        {
            _meetingRepository = repository;
            _logger = logger;
        }

        public async Task AddAsync(Meeting obj)
        {
            //await _meetingRepository.AddObjectAsync(obj);
            try
            {
                await _meetingRepository.AddObjectAsync(obj);
                _logger.LogInfo("Meeting added well.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw;
            }
        }

        public async Task<Meeting> GetByIdAsync(int id)
        {
            //var meeting = await _meetingRepository.GetObjectByIdAsync(id);
            //if (meeting != null)
            //    return meeting;
            //return null;
            try
            {
                var meeting = await _meetingRepository.GetObjectByIdAsync(id);
                if (meeting != null)
                {
                    _logger.LogInfo($"Meeting with ID {id} retrieved well.");
                    return meeting;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw;
            }
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
