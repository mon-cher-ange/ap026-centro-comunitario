using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public interface IStudentRepository
    {
        Student FindById(int id);
        Student FindByDNI(string dni);
        IEnumerable<Student> GetAllStudents();
        void Add(Student student);
        bool Update(Student student);
        bool Remove(int id);
    }
}
