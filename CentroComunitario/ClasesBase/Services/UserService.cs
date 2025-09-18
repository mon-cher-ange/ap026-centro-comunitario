using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasesBase.Interfaces;
using ClasesBase.Repositories;
using ClasesBase;

namespace ClasesBase.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(int userId)
        {
            return _userRepository.FindById(userId);
        }

        public User GetUserByUsername(string userUsername)
        {
            return _userRepository.FindByUsername(userUsername);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void DeleteUser(int userId)
        {
            _userRepository.Remove(userId);
        }
    }
}
