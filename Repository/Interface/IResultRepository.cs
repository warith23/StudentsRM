using System.Linq.Expressions;
using StudentsRM.Entities;

namespace StudentsRM.Repository.Interface
{
    public interface IResultRepository : IRepository<Result>
    {
        Result GetResult(Expression<Func<Result, bool>> expression);
    }
}