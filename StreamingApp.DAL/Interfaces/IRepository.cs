using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task AddObjectAsync(T obj);
        Task<T> GetObjectByIdAsync(int id);
        Task<IEnumerable<T>> GetAllObjectsAsync();
        Task UpdateObjectAsync(T obj);
        Task DeleteObjectAsync(int id);
    }
}
