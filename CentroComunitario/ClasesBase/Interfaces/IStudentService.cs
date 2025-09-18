using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase.Interfaces
{
    public interface IStudentService
    {
        Student GetStudentById(int studentId);
        Student GetStudentByDni(string studentDni);
        IEnumerable<Student> GetAllStudents();
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
    }
}
