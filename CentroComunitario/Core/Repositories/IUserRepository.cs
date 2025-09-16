using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        User FindById(int id);
        User FindByUsernameAndPassword(string username, string password);
        IEnumerable<User> GetAllUsers();
        void Add(User user);
        bool Update(User user);
        bool Remove(int id);
    }
}
