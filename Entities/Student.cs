using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsRM.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateAdmitted { get; set; }
        public ICollection<StudentCourse> Courses { get; set; } = new HashSet<StudentCourse>();
        public ICollection<Result> Results { get; set; } = new HashSet<Result>();
    }
}