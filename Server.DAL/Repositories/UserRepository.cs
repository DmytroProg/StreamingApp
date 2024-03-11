using Server.DAL.Implementations;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Server.DAL.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository() : base()
    {
    }

    public async Task<User> FindByLogin(string login, string password)
    {
        return (await _dbContext.User.FirstOrDefaultAsync(u => u.Login == login 
        && u.Password == password)) ?? throw new ArgumentNullException();
    }
}
