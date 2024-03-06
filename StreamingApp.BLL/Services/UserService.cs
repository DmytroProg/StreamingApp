using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Services
{
    public class UserService : IService<User>
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task AddAsync(User obj)
        {
            await _userRepository.AddObjectAsync(obj);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetObjectByIdAsync(id);
            if (user != null)
                return user;
            return null;
        }

        public async Task<IEnumerable<User>> QueryMany(Predicate<User> query)
        {
            return (await _userRepository.GetAllObjectsAsync())
                .Where(user => query(user));
        }

        public async Task<User> QueryOne(Predicate<User> query)
        {
            var data = (await _userRepository.GetAllObjectsAsync())
                .FirstOrDefault(user => query(user));
            return data;
        }
    }
}
