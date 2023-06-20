using System.Linq.Expressions;
using StudentsRM.Entities;

namespace StudentsRM.Repository.Interface
{
    public interface IStudentRepository : IRepository<Student>
    {
        List<Course> GetStudentByCourseId(string courseId);
        Student GetStudentResult(Expression<Func<Student, bool>> expression);
        List<Student> GetAllStudent(Expression<Func<Student, bool>> expression);
        List<Student> GetAllStudentWithoutResult(string semesterId, string lecturerCourseId);
    }
}