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

        public List<Course> GetStudentByCourseId(string courseId)
        {
            var students = _context.Courses
            .Where(c => c.Id.Equals(courseId))
            .ToList();
            return students;
        }

        public Student GetStudentResult(Expression<Func<Student, bool>> expression)
        {
            var student = _context.Students
                .Where(expression)
                .Include(s => s.Course)
                .Include(s => s.Results)
                .SingleOrDefault(expression);

            return student;
        }

        public List<Student> GetAllStudent(Expression<Func<Student, bool>> expression)
        {
            var students = _context.Students
                .Where(expression)
                .Include(s => s.Course)
                .ToList();

            return students;
        }

        public List<Student> GetAllStudentWithoutResult(string semesterId, string lecturerCourseId)
        {
            var students = _context.Students
                .Where(s => !s.Results
                .Any(r => r.StudentId == s.Id && r.SemesterId == semesterId && r.CourseId == lecturerCourseId))
                .ToList();
            return students;
        }
    }
}