using Server.DAL.Entities;
using Server.DAL.Interfaces;
using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using Server.DAL.Implementations;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Interfaces.DataAccess;

namespace Server.DAL.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository() : base()
    {
    }
}
