using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Services
{
    public class UserService : IService<User>
    {
        private IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IUserRepository repository, ILogger logger)
        {
            _userRepository = repository;
            _logger = logger;
        }

        public async Task AddAsync(User obj)
        {
            //await _userRepository.AddObjectAsync(obj);
            try
            {
                await _userRepository.AddObjectAsync(obj);
                _logger.LogInfo("User added well.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            //var user = await _userRepository.GetObjectByIdAsync(id);
            //if (user != null)
            //    return user;
            //return null;
            try
            {
                var user = await _userRepository.GetObjectByIdAsync(id);
                if (user != null)
                {
                    _logger.LogInfo($"User with ID {id} retrieved well.");
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw;
            }
        }

        public async Task<IEnumerable<User>> QueryMany(Predicate<User> query)
        {
            //return (await _userRepository.GetAllObjectsAsync())
            //    .Where(user => query(user));
            try
            {
                return (await _userRepository.GetAllObjectsAsync())
                    .Where(user => query(user));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw;
            }
        }

        public async Task<User> QueryOne(Predicate<User> query)
        {
            //var data = (await _userRepository.GetAllObjectsAsync())
            //    .FirstOrDefault(user => query(user));
            //return data;
            try
            {
                var data = (await _userRepository.GetAllObjectsAsync())
                .FirstOrDefault(user => query(user));
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw;
            }
        }
    }
}
