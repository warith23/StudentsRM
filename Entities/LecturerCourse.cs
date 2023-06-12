using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsRM.Entities
{
    public class LecturerCourse
    {
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public string LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
    }
}