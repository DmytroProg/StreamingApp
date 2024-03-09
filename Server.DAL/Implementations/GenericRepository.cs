using Microsoft.EntityFrameworkCore;
using Server.DAL.Entities;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;

namespace Server.DAL.Implementations;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly InMemoryDbContext _dbContext;

    public GenericRepository()
    {
        _dbContext = new InMemoryDbContext();
    }
    public async Task<T> AddObjectAsync(T obj)
    {
        var item = _dbContext.Set<T>().Add(obj);
        await _dbContext.SaveChangesAsync();
        return item.Entity;
    }

    public async Task DeleteObjectAsync(int id)
    {
        _dbContext.Set<T>().Remove(await GetObjectByIdAsync(id));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllObjectsAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetObjectByIdAsync(int id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id)
            ?? throw new ArgumentNullException();
        return entity;
    }

    public async Task<T> QueryOne(Predicate<T> predicate)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(t => predicate(t))
            ?? throw new ArgumentNullException();
    }

    public async Task UpdateObjectAsync(T obj)
    {
        _dbContext.Set<T>().Update(obj);
        await _dbContext.SaveChangesAsync();
    }
}
