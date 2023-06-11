using StudentsRM.Entities;
using StudentsRM.Repository.Interface;
using StudentsRM.Context;

namespace StudentsRM.Repository.Implementation
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(StudentsRMContext context) : base(context)
        {

        }
    }
}