namespace StreamingApp.BLL.Interfaces.DataAccess;

public interface IRepository<T>
{
    Task<T> AddObjectAsync(T obj);
    Task<T> GetObjectByIdAsync(int id);
    Task<IEnumerable<T>> GetAllObjectsAsync();
    Task UpdateObjectAsync(T obj);
    Task DeleteObjectAsync(int id);
    Task<T> QueryOne(Predicate<T> predicate);
}
