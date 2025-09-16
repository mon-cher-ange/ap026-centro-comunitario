using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public interface ICourseRepository
    {
        Course FindById(int id);
        Course FindByName(string name);
        IEnumerable<Course> GetAllCourses();
        void Add(Course course);
        bool Update(Course course);
        bool Remove(int id);
    }
}
