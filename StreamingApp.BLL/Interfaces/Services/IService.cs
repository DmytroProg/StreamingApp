using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Interfaces.Services
{
    public interface IService<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T obj);
    }
}
