using Microsoft.EntityFrameworkCore;
using Server.DAL.Entities;
using Server.DAL.Interfaces.Base;

namespace Server.DAL.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly InMemoryDbContext _dbContext;

    public GenericRepository()
    {
        _dbContext = new InMemoryDbContext();
    }
    public async Task AddObjectAsync(T obj)
    {
        _dbContext.Set<T>().Add(obj); 
        await _dbContext.SaveChangesAsync();
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

    public async Task UpdateObjectAsync(T obj)
    {
        _dbContext.Set<T>().Update(obj);
        await _dbContext.SaveChangesAsync();
    }
}
