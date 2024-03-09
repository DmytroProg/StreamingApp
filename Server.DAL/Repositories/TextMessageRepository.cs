﻿using Server.DAL.Entities;
using Server.DAL.Implementations;
using Server.DAL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;

namespace Server.DAL.Repositories;

public class TextMessageRepository : GenericRepository<TextMessage>, ITextMessageRepository
{
    public TextMessageRepository() : base() { }

    public Task<IEnumerable<TextMessage>> GetTextMessagesByUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}