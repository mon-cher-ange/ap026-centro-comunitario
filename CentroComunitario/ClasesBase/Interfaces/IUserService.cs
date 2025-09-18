using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasesBase;

namespace ClasesBase.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int userId);
        User GetUserByUsername(string userUsername);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
    }
}
