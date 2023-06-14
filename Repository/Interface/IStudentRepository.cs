using StudentsRM.Entities;

namespace StudentsRM.Repository.Interface
{
    public interface IStudentRepository : IRepository<Student>
    {
        public string GetStudentCourse(Course course);
    }
}