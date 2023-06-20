using System.Linq.Expressions;
using StudentsRM.Entities;

namespace StudentsRM.Repository.Interface
{
    public interface ILecturerRepository : IRepository<Lecturer>
    {
        List<Lecturer> GetAllLecturers(Expression<Func<Lecturer, bool>> expression);        
    }
}