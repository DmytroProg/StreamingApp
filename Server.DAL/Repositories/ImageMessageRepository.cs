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
    public class ImageMessageRepository : GenericRepository<ImageMessage>, IImageMessageRepository
    {
        public ImageMessageRepository() : base() { }

        public Task<IEnumerable<ImageMessage>> GetImageMessagesByUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
