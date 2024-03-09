using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Interfaces.Services;

namespace StreamingApp.BLL.Implementations;

public class GenericService<T> : IService<T> where T : class
{
    protected readonly IRepository<T> _repository;
    protected string _modelName;
    protected readonly ILogger _logger;

    public GenericService(IRepository<T> repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<T> AddAsync(T obj)
    {
        try
        {
            var item = await _repository.AddObjectAsync(obj);
            _logger.LogInfo($"{_modelName} has been added successfully.");
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
            throw;
        }
    }

    public async Task<T> GetByIdAsync(int id)
    {
        try
        {
            var item = await _repository.GetObjectByIdAsync(id);
            if (item != null)
            {
                _logger.LogInfo($"{_modelName} with ID {id} has been retrieved successfully.");
                return item;
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
            throw;
        }
    }

    public Task<T> QueryOne(Predicate<T> query)
    {
        return _repository.QueryOne(query);
    }
}
