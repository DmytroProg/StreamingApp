using Server.DAL.Implementations;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;

namespace Server.DAL.Repositories;

public class MeetingRepository : GenericRepository<Meeting>, IMeetingRepository
{
    public MeetingRepository() : base()
    {
    }

    public async Task AddUserToMeetingAsync(int id, User user)
    {
        var meeting = await GetObjectByIdAsync(id);
        meeting.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }
}
