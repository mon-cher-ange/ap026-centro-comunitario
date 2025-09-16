using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Interfaces;
using Core.Repositories;

namespace Core.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public Teacher GetTeacherById(int teacherId)
        {
            return _teacherRepository.FindById(teacherId);
        }

        public Teacher GetTeacherByDni(string teacherDni)
        {
            return _teacherRepository.FindByDNI(teacherDni);
        }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            return _teacherRepository.GetAllTeachers();
        }

        public void AddTeacher(Teacher teacher)
        {
            _teacherRepository.Add(teacher);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            _teacherRepository.Update(teacher);
        }

        public void DeleteTeacher(int teacherId)
        {
            _teacherRepository.Remove(teacherId);
        }
    }
}
