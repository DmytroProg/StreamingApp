using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Interfaces
{
    public interface IServise<T>
    {
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T obj);
    }
}
