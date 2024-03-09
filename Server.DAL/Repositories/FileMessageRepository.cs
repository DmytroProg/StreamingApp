using Server.DAL.Implementations;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;

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
