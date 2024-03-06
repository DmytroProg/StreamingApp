namespace Server.DAL.Interfaces.Base;

public interface IGenericRepository<T>
{
    Task AddObjectAsync(T obj);
    Task<T> GetObjectByIdAsync(int id);
    Task<IEnumerable<T>> GetAllObjectsAsync();
    Task UpdateObjectAsync(T obj);
    Task DeleteObjectAsync(int id);
}
