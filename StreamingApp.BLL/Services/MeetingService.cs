using StreamingApp.BLL.Implementations;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Interfaces.Services;
using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Services;

public class MeetingService : GenericService<Meeting>, IMeetingService
{
    public MeetingService(IMeetingRepository repository, ILogger logger) 
        : base(repository, logger)
    {
        _modelName = nameof(Meeting);
    }

    public async Task AddUserToMeetingAsync(int id, User user)
    {
        try
        {
            if(_repository is IMeetingRepository repo)
            {
                await repo.AddUserToMeetingAsync(id, user);
                _logger.LogInfo($"Added user {user.Name} to meeting with id {id}");
            }
            else
            {
                var ex = new ArgumentException(nameof(user), "invalid user was passed");
                _logger.LogError(ex);
                throw ex;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
            throw;
        }
    }
}
