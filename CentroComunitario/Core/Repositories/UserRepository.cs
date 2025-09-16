using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users;
        private int _nextId;

        public UserRepository()
        {
            _users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "admin-user",
                    Password = "admin-passwd",
                    FullName = "John Doe",
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    Username = "teacher-user",
                    Password = "teacher-passwd",
                    FullName = "Mary Johnson",
                    RoleId = 2
                },
                new User
                {
                    Id = 3,
                    Username = "receptionist-user",
                    Password = "receptionist-passwd",
                    FullName = "Maria Garcia",
                    RoleId = 3
                }
            };

            _nextId = _users.Max(u => u.Id) + 1;
        }

        public User FindById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public User FindByUsernameAndPassword(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
