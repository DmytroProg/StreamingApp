using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.DAL.Entities;
using Server.DAL.Models;
using Server.DAL.Repositories;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Retranslators;

namespace StreamingApp.BLL.Services
{
    public class UserServise : IServise<User>
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
    }
}
