using Server.DAL.Entities;
using Server.DAL.Interfaces;
using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Repositories
{
    public class UserRepository : IRepository<UserInfo>
    {
        private InMemoryDbContext _context;
        public UserRepository()
        {
            _context = new InMemoryDbContext();
        }
        public async Task AddObjectAsync(UserInfo obj)
        {
            await _context.User.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteObjectAsync(int id)
        {
            var obj = await _context.User.FindAsync(id);
            _context.User.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserInfo>> GetAllObjectsAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<UserInfo> GetObjectByIdAsync(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task UpdateObjectAsync(UserInfo obj)
        {
            _context.User.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
