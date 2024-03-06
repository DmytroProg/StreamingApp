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
    public class MeetingRepository : IRepository<MeetingInfo>
    {
        private InMemoryDbContext _context;
        public MeetingRepository() 
        { 
            _context = new InMemoryDbContext();
        }
        public async Task AddObjectAsync(MeetingInfo obj)
        {
            await _context.Meeting.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteObjectAsync(int id)
        {
            var obj = await _context.Meeting.FindAsync(id);
            _context.Meeting.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MeetingInfo>> GetAllObjectsAsync()
        {
            return await _context.Meeting.ToListAsync();
        }

        public async Task<MeetingInfo> GetObjectByIdAsync(int id)
        {
            return await _context.Meeting.FindAsync(id);
        }

        public async Task UpdateObjectAsync(MeetingInfo obj)
        {
            _context.Meeting.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
