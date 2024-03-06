using Server.DAL.Entities;
using Server.DAL.Implementations;
using Server.DAL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;

namespace Server.DAL.Repositories;

public class MessageRepository : GenericRepository<TextMessage>, IMessageRepository
{
    public MessageRepository() : base()
    {
    }
}
