using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StudentsRM.Entities;

namespace StudentsRM.Context
{
    public class StudentsRMContext : DbContext
    {
        public StudentsRMContext(DbContextOptions<StudentsRMContext> options) : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // }

        // public override int SaveChanges()
        // {
        //     var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        //     foreach (var entry in entries)
        //     {
        //         if (entry.State == EntityState.Added)
        //         {
        //             ((BaseEntity)entry.Entity).DateCreated = DateTime.Now;
        //         }

        //         if (entry.State == EntityState.Modified)
        //             ((BaseEntity)entry.Entity).LastModified = DateTime.Now;
        //     }

        //     foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is ISoftDeletable && e.State == EntityState.Deleted))
        //     {
        //         entry.State = EntityState.Modified;
        //         entry.CurrentValues["IsDeleted"] = true;
        //     }

        //     return base.SaveChanges();
        // }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}