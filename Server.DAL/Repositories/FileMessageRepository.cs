using Server.DAL.Implementations;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Repositories
{
    public class FileMessageRepository : GenericRepository<FileMessage>, IFileMessageRepository
    {
        public FileMessageRepository() : base() { }

        public Task<IEnumerable<FileMessage>> GetFileMessagesByUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
