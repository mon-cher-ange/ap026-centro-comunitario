using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private List<Teacher> _teachers;
        private int _nextId;

        public TeacherRepository()
        {
            _teachers = new List<Teacher>
            {
                new Teacher
                {
                    Id = 1,
                    DNI = "1234567",
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@doe.com"
                },
                new Teacher
                {
                    Id = 2,
                    DNI = "2345678",
                    Name = "Jane",
                    LastName = "Smith",
                    Email = "jane@smith.com"
                },
                new Teacher
                {
                    Id = 3,
                    DNI = "3456789",
                    Name = "Alice",
                    LastName = "Johnson",
                    Email = "alice@johnson.com"
                }
            };

            _nextId = _teachers.Max(s => s.Id) + 1;
        }

        public Teacher FindById(int id)
        {
            return _teachers.FirstOrDefault(t => t.Id == id);
        }

        public Teacher FindByDNI(string dni)
        {
            return _teachers.FirstOrDefault(t => t.DNI == dni);
        }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            return _teachers;
        }

        public void Add(Teacher teacher)
        {
            if (FindByDNI(teacher.DNI) != null)
            {
                throw new InvalidOperationException("A teacher with this DNI already exists.");
            }

            teacher.Id = _nextId++;
            _teachers.Add(teacher);
        }

        public bool Update(Teacher teacher)
        {
            Teacher teacherToUpdate = FindById(teacher.Id);
            if (teacherToUpdate == null) return false;


            Teacher existingTeacherWithDNI = FindByDNI(teacher.DNI);

            if (existingTeacherWithDNI != null && existingTeacherWithDNI.Id != teacher.Id)
            {
                throw new InvalidOperationException("A teacher with this DNI already exists.");
            }

            teacherToUpdate.DNI = teacher.DNI;
            teacherToUpdate.Name = teacher.Name;
            teacherToUpdate.LastName = teacher.LastName;
            teacherToUpdate.Email = teacher.Email;

            return true;
        }

        public bool Remove(int id)
        {
            Teacher teacherToDelete = FindById(id);

            if (teacherToDelete == null) return false;

            _teachers.Remove(teacherToDelete);

            return true;
        }
    }
}
