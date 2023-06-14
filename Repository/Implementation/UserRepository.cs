using StudentsRM.Entities;
using StudentsRM.Repository.Interface;
using StudentsRM.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace StudentsRM.Repository.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(StudentsRMContext context) : base(context)
        {

        }

        public User GetUser(Expression<Func<User, bool>> expression)
        {
            return _context.Users
                .Include(x => x.Role)
                .SingleOrDefault(expression);
        }
    }
}