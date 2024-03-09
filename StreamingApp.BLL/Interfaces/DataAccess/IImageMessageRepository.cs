using StreamingApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Interfaces.DataAccess
{
    public interface IImageMessageRepository : IRepository<ImageMessage>
    {
        Task<IEnumerable<ImageMessage>> GetImageMessagesByUserAsync(User user);
    }
}
