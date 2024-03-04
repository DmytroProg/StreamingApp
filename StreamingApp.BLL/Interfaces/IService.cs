using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Interfaces
{
    public interface IService<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> QueryOne(Predicate<T> query);
        Task<IEnumerable<T>> QueryMany(Predicate<T> query);
        Task AddAsync(T obj);
    }
}
