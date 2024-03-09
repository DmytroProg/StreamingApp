using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Interfaces.DataAccess;

public interface IMeetingRepository : IRepository<Meeting>
{
    Task AddUserToMeetingAsync(int id, User user);
}
