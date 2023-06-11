using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsRM.Models.Results
{
    public class AddResultViewModel
    {
        
        public List<string> StudentId { get; set; }
        public int Score { get; set; }
        public List<string> SemesterId { get; set; }
        public List<string> CourseId { get; set; }
    }
}