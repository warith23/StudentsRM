using StudentsRM.Entities;
using StudentsRM.Repository.Interface;
using StudentsRM.Context;

namespace StudentsRM.Repository.Implementation
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentsRMContext context) : base(context)
        {

        }
    }
}