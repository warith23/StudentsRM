using StudentsRM.Entities;
using StudentsRM.Repository.Interface;
using StudentsRM.Context;

namespace StudentsRM.Repository.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(StudentsRMContext context) : base(context)
        {

        }
        
    }
}