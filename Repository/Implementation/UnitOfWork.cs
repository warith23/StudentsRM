using StudentsRM.Context;
using StudentsRM.Repository.Interface;

namespace StudentsRM.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentsRMContext _context;
        public bool _disposed = false;
        public IRoleRepository Roles { get; }

        public ICourseRepository Courses { get; }

        public ILecturerRepository Lecturers {get;}

        public IResultRepository Results { get; }

        public ISemesterRepository Semesters { get; }

        public IStudentRepository Students { get; }

        public IUserRepository Users { get; }

        public UnitOfWork(
            StudentsRMContext context,
            IRoleRepository roleRepository,
            ICourseRepository courseRepository,
            ILecturerRepository lecturerRepository,
            IResultRepository resultRepository,
            ISemesterRepository semesterRepository,
            IStudentRepository studentRepository,
            IUserRepository userRepository
        )
        {
            _context = context;
            Roles = roleRepository;
            Courses = courseRepository;
            Lecturers = lecturerRepository;
            Results = resultRepository;
            Semesters = semesterRepository;
            Students = studentRepository;
            Users = userRepository;
        } 

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}