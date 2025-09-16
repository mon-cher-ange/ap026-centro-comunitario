using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private List<Course> _courses;
        private int _nextId;

        public CourseRepository()
        {
            _courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Mathematics",
                    Description = "Basic Mathematics Course",
                    Quota = 30,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(3),
                    StateId = 1,
                    TeacherId = 1
                },
                new Course
                {
                    Id = 2,
                    Name = "Physics",
                    Description = "Introduction to Physics",
                    Quota = 25,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(4),
                    StateId = 2,
                    TeacherId = 2
                },
                new Course
                {
                    Id = 3,
                    Name = "Chemistry",
                    Description = "Basic Chemistry Course",
                    Quota = 20,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(3),
                    StateId = 3,
                    TeacherId = 3
                },
                new Course
                {
                    Id = 4,
                    Name = "Biology",
                    Description = "Introduction to Biology",
                    Quota = 15,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(4),
                    StateId = 4,
                    TeacherId = 2
                }
            };

            _nextId = _courses.Max(c => c.Id) + 1;
        }

        public Course FindById(int id)
        {
            return _courses.FirstOrDefault(c => c.Id == id);
        }

        public Course FindByName(string name)
        {
            return _courses.FirstOrDefault(c => c.Name == name);
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _courses;
        }

        public void Add(Course course)
        {
            if (FindByName(course.Name) != null)
            {
                throw new InvalidOperationException("A course with this Name already exists.");
            }

            course.Id = _nextId++;
            _courses.Add(course);
        }

        public bool Update(Course course)
        {
            Course courseToUpdate = FindById(course.Id);
            if (courseToUpdate == null) return false;


            Course existingCourseWithName = FindByName(course.Name);

            if (existingCourseWithName != null && existingCourseWithName.Id != course.Id)
            {
                throw new InvalidOperationException("A course with this Name already exists.");
            }

            courseToUpdate.Name = course.Name;
            courseToUpdate.Description = course.Description;
            courseToUpdate.Quota = course.Quota;
            courseToUpdate.StartDate = course.StartDate;
            courseToUpdate.EndDate = course.EndDate;
            courseToUpdate.StateId = course.StateId;
            courseToUpdate.TeacherId = course.TeacherId;

            return true;
        }

        public bool Remove(int id)
        {
            Course courseToDelete = FindById(id);

            if (courseToDelete == null) return false;

            _courses.Remove(courseToDelete);

            return true;
        }
    }
}
