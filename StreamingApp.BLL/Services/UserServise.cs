using Server.DAL.Repositories;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Retranslators;

namespace StreamingApp.BLL.Services
{
    public class UserServise : IService<User>
    {
        private UserRepository _userRepository;
        private UserRetranslator _userRetranslator;

        public UserServise()
        {
            _userRepository = new UserRepository();
            _userRetranslator = new UserRetranslator();
        }

        public async Task AddAsync(User obj)
        {
            await _userRepository.AddObjectAsync(_userRetranslator.TranslateUserToUserInfo(obj));
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetObjectByIdAsync(id);
            if (user != null)
                return _userRetranslator.TranslateUserInfoToUser(user);
            return null;
        }

        public async Task<IEnumerable<User>> QueryMany(Predicate<User> query)
        {
            return (await _userRepository.GetAllObjectsAsync())
                .Where(user => query(_userRetranslator.TranslateUserInfoToUser(user)))
                .Select(_userRetranslator.TranslateUserInfoToUser);
        }

        public async Task<User> QueryOne(Predicate<User> query)
        {
            var data = (await _userRepository.GetAllObjectsAsync())
                .FirstOrDefault(user => query(_userRetranslator.TranslateUserInfoToUser(user)));
            return _userRetranslator.TranslateUserInfoToUser(data);
        }
    }
}
