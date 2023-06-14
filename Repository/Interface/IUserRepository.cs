using StudentsRM.Entities;
using System.Linq.Expressions;

namespace StudentsRM.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public User GetUser(Expression<Func<User, bool>> expression);   
    }
}