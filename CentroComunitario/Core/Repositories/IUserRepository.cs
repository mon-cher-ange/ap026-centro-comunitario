using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        User GetById(int userId);
        User GetByUsernameAndPassword(string username, string password);
        IEnumerable<User> GetAll();
        void Add(User user);
    }
}
