using StudentsRM.Entities;
using StudentsRM.Repository.Interface;
using StudentsRM.Context;

namespace StudentsRM.Repository.Implementation
{
    public class ResultRepository : BaseRepository<Result>, IResultRepository
    {
        public ResultRepository(StudentsRMContext context) : base(context)
        { 
        }
    }
}