using StudentsRM.Entities;
using StudentsRM.Helper;

namespace StudentsRM.Context
{
    internal class DbInitializer
    {
        internal static void Initialize(StudentsRMContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }

            var roles = new Role[]
            {
                new Role()
                {
                    RoleName = "Admin",
                    Description = "Role for admin",
                    RegisteredBy = "System",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    ModifiedBy = "",
                    LastModified = new DateTime() //0001-01-01 00:00:00:00
                },
                new Role()
                {
                    RoleName = "Student",
                    Description = "Role for Student user",
                    RegisteredBy = "System",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    ModifiedBy = "",
                    LastModified = new DateTime()
                },
                new Role()
                {
                    RoleName = "Lecturer",
                    Description = "Role for Lecturer user",
                    RegisteredBy = "System",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    ModifiedBy = "",
                    LastModified = new DateTime()
                }
            };

            foreach (var r in roles)
            {
                context.Roles.Add(r);
            }

            context.SaveChanges();

            var password = "password1";
            var salt = HashingHelper.GenerateSalt();
            var admin = context.Roles.Where(r => r.RoleName == "Admin").SingleOrDefault();

            var users = new User[]
            {
                new User()
                {
                    Email = "admin@gmail.com",
                    HashSalt = salt,
                    PasswordHash = HashingHelper.HashPassword(password, salt),
                    RoleId = admin.Id,
                    Role = admin,
                    RegisteredBy = "System",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    ModifiedBy = "",
                    LastModified = new DateTime(),
                }
            };

            foreach (var u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();
        }
    }
}
