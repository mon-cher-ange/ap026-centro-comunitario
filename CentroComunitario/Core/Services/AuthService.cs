using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Interfaces;
using Core.Repositories;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string username, string password)
        {
            User user = _userRepository.FindByUsernameAndPassword(username, password);
            if (user == null) return false;

            return true;
        }
    }
}
