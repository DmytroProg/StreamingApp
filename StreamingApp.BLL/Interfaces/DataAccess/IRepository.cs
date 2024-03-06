namespace StreamingApp.BLL.Interfaces.DataAccess;

public interface IRepository<T>
{
    Task AddObjectAsync(T obj);
    Task<T> GetObjectByIdAsync(int id);
    Task<IEnumerable<T>> GetAllObjectsAsync();
    Task UpdateObjectAsync(T obj);
    Task DeleteObjectAsync(int id);
}
