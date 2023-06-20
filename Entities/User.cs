using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsRM.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }  
        public string HashSalt { get; set; }
        public string PasswordHash { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        // public string LecturerStudentId { get; set; }
        public Student? Student { get; set; }
        public string? StudentId { get; set; }
        public Lecturer? Lecturer { get; set; }
        public string? LecturerId { get; set; }
    }
}