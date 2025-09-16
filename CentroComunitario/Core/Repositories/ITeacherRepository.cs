using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public interface ITeacherRepository
    {
        Teacher FindById(int id);
        Teacher FindByDNI(string dni);
        IEnumerable<Teacher> GetAllTeachers();
        void Add(Teacher teacher);
        bool Update(Teacher teacher);
        bool Remove(int id);
    }
}
