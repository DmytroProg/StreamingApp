using Server.DAL.Implementations;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;

namespace Server.DAL.Repositories
{
    public class ImageMessageRepository : GenericRepository<ImageMessage>, IImageMessageRepository
    {
        public ImageMessageRepository() : base() { }

        public Task<IEnumerable<ImageMessage>> GetImageMessagesByUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
