using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users;

        public UserRepository()
        {
            _users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "a",
                    Password = "a",
                    FullName = "John Doe",
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    Username = "teacher-user",
                    Password = "teacher-user-passwd",
                    FullName = "Mary Johnson",
                    RoleId = 2
                },
                new User
                {
                    Id = 3,
                    Username = "receptionist-user",
                    Password = "receptionist-user-passwd",
                    FullName = "Maria Garcia",
                    RoleId = 3
                }

            };
        }

        public User GetById(int userId)
        {
            foreach (User user in _users)
            {
                if (user.Id == userId)
                    return user;
            }
            return null;
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            foreach (User user in _users)
            {
                if (user.Username == username && user.Password == password)
                    return user;
            }
            return null;
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public void Add(User user)
        {
            user.Id = _users.Count() + 1;
            _users.Add(user);
        }
    }
}
