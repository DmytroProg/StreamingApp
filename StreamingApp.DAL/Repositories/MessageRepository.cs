using Microsoft.EntityFrameworkCore;
using Server.DAL.Entities;
using Server.DAL.Interfaces;
using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Repositories
{
    public class MessageRepository //: IRepository<MessageBaseInfo>
    {
        private InMemoryDbContext _context;
        public MessageRepository()
        {
            _context = new InMemoryDbContext();
        }
        //public async Task AddObjectAsync(MessageBaseInfo obj)
        //{
        //    await _context.Message.AddAsync(obj);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteObjectAsync(int id)
        //{
        //    var obj = await _context.Message.FindAsync(id);
        //    _context.Message.Remove(obj);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<MessageBaseInfo>> GetAllObjectsAsync()
        //{
        //    return await _context.Message.ToListAsync();
        //}

        //public async Task<MessageBaseInfo> GetObjectByIdAsync(int id)
        //{
        //    return await _context.Message.FindAsync(id);
        //}

        //public async Task UpdateObjectAsync(MessageBaseInfo obj)
        //{
        //    _context.Message.Update(obj);
        //    await _context.SaveChangesAsync();
        //}
    }
}
