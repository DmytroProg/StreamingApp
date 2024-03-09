using StreamingApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Interfaces.DataAccess
{
    public interface IFileMessageRepository : IRepository<FileMessage>
    {
        Task<IEnumerable<FileMessage>> GetFileMessagesByUserAsync(User user);
    }
}
