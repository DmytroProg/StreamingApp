using Microsoft.EntityFrameworkCore;
using Server.DAL.Entities;
using Server.DAL.Implementations;
using Server.DAL.Interfaces;
using Server.DAL.Models;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;

namespace Server.DAL.Repositories;

public class MeetingRepository : GenericRepository<Meeting>, IMeetingRepository
{
    public MeetingRepository() : base()
    {
    }
}
