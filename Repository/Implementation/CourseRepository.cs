using StudentsRM.Entities;
using StudentsRM.Repository.Interface;
using StudentsRM.Context;

namespace StudentsRM.Repository.Implementation
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(StudentsRMContext context) : base(context)
        { 
        }
        
    }
}