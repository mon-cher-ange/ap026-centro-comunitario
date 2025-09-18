using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase.Repositories
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
                    Id = 0,
                    Username = "test",
                    Password = "pass",
                    FullName = "test-pass",
                    RoleId = 1
                },
                new User
                {
                    Id = 1,
                    Username = "admin",
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

        public User FindByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
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
            if (FindByUsername(user.Username) != null)
            {
                throw new InvalidOperationException(
                    "El nombre de usuario ya está registrado"
                );
            }

            user.Id = _nextId++;
            _users.Add(user);
        }

        public bool Update(User user)
        {
            User userToUpdate = FindById(user.Id);
            if (userToUpdate == null) return false;

            User existingUserWithUsername = FindByUsername(user.Username);

            if (FindByUsername(user.Username) != null &&
                existingUserWithUsername.Id != user.Id)
            {
                throw new InvalidOperationException(
                    "El nombre de usuario ya está registrado"
                );
            }

            userToUpdate.Username = user.Username;
            userToUpdate.Password = user.Password;
            userToUpdate.FullName = user.FullName;
            userToUpdate.RoleId = user.RoleId;

            return true;
        }

        public bool Remove(int id)
        {
            User userToDelete = FindById(id);

            if (userToDelete == null) return false;

            _users.Remove(userToDelete);

            return true;
        }
    }
}
