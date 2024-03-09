using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Interfaces.DataAccess;

public interface IUserRepository : IRepository<User>
{
    Task<User> QueryOne(Predicate<User> predicate);
}
