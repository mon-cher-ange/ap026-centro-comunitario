using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase.Interfaces
{
    public interface ICourseService
    {
        Course GetCourseById(int courseId);
        Course GetCourseByName(string courseName);
        IEnumerable<Course> GetAllCourses();
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int courseId);
    }
}
