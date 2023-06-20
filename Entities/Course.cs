using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsRM.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public List<Lecturer> Lecturer { get; set; }
        public List<Student> Student { get; set; }
        
    }
}