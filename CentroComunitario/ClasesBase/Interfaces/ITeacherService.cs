using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase.Interfaces
{
    public interface ITeacherService
    {
        Teacher GetTeacherById(int teacherId);
        Teacher GetTeacherByDni(string teacherDni);
        IEnumerable<Teacher> GetAllTeachers();
        void AddTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(int teacherId);
    }
}
