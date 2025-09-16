using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private List<Student> _students;
        private int _nextId;

        public StudentRepository()
        {
            _students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    DNI = "654987",
                    Name = "Charles",
                    LastName = "Peterson",
                    Email = "charles@peterson.com"
                },
                new Student
                {
                    Id = 2,
                    DNI = "123456",
                    Name = "Ruth",
                    LastName = "Harper",
                    Email = "ruth@harper.com"
                },
                new Student
                {
                    Id = 3,
                    DNI = "987542",
                    Name = "Maria",
                    LastName = "Madrigal",
                    Email = "maria@madrigal.com"
                }
            };

            _nextId = _students.Max(s => s.Id) + 1;
        }
        public Student FindById(int id)
        {
            return _students.FirstOrDefault(u => u.Id == id);
        }

        public Student FindByDNI(string dni)
        {
            return _students.FirstOrDefault(u => u.DNI== dni);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

        public void Add(Student student)
        {
            if (FindByDNI(student.DNI) != null)
            {
                throw new InvalidOperationException("A student with this DNI already exists.");
            }

            student.Id = _nextId++;
            _students.Add(student);
        }

        public bool Update(Student student)
        {
            Student studentToUpdate = FindById(student.Id);
            if (studentToUpdate == null) return false;


            Student existingStudentWithDNI = FindByDNI(student.DNI);

            if (existingStudentWithDNI != null && existingStudentWithDNI.Id != student.Id)
            {
                throw new InvalidOperationException("A student with this DNI already exists.");
            }

            studentToUpdate.DNI = student.DNI;
            studentToUpdate.Name = student.Name;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.Email = student.Email;

            return true;
        }

        public bool Remove(int id)
        {
            Student studentToDelete = FindById(id);

            if (studentToDelete == null) return false;

            _students.Remove(studentToDelete);

            return true;
        }
    }
}
