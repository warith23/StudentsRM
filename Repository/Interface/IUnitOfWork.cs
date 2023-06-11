namespace StudentsRM.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRoleRepository Roles { get; }
        ICourseRepository Courses { get; }
        ILecturerRepository Lecturers { get; }
        IResultRepository Results { get; }
        ISemesterRepository Semesters { get; }
        IStudentRepository Students { get; }
        IUserRepository Users { get; }
        int SaveChanges();
    }
}