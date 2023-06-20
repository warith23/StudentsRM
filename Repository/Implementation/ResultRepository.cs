using StudentsRM.Entities;
using StudentsRM.Repository.Interface;
using StudentsRM.Context;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace StudentsRM.Repository.Implementation
{
    public class ResultRepository : BaseRepository<Result>, IResultRepository
    {
        public ResultRepository(StudentsRMContext context) : base(context)
        { 
        }

        public Result GetResult(Expression<Func<Result, bool>> expression)
        {
            var result = _context.Results
                .Where(expression)
                .Include(s => s.Course)
                .Include(s => s.Semester)
                .Include(s => s.Student)
                .FirstOrDefault();

            return result;
        }
    }
}