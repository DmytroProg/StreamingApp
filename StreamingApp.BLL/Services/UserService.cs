using StreamingApp.BLL.Implementations;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Interfaces.Services;
using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Services;

public class UserService : GenericService<User>, IUserService
{
    public UserService(IRepository<User> repository, ILogger logger) 
        : base(repository, logger)
    {
    }

    public Task<User> GetByLoginAsync(string login, string password)
    {
        return (_repository as IUserRepository).FindByLogin(login, password);
    }
}
