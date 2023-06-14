using StudentsRM.Entities;
using StudentsRM.Repository.Interface;
using StudentsRM.Context;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace StudentsRM.Repository.Implementation
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentsRMContext context) : base(context)
        {
            
        }

        public string GetStudentCourse(Course course)
        {
            var students = _context.StudentsCourses
            .Where(c => c.Course == course)
            .Select(c => c.CourseId).ToString();
            return students;
        }
    }
}