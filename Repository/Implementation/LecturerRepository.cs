using StudentsRM.Entities;
using StudentsRM.Repository.Interface;
using StudentsRM.Context;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace StudentsRM.Repository.Implementation
{
    public class LecturerRepository : BaseRepository<Lecturer>, ILecturerRepository
    {
        public LecturerRepository(StudentsRMContext context) : base(context)
        { 
        }

        public List<Lecturer> GetAllLecturers(Expression<Func<Lecturer, bool>> expression)
        {
            var lecturers = _context.Lecturers
                .Where(expression)
                .Include(s => s.Course)
                .ToList();

            return lecturers;
        }
        
    }
}