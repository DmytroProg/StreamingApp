using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Interfaces.DataAccess;

public interface IUserRepository : IRepository<User>
{
    Task<User> FindByLogin(string login, string password);
}
