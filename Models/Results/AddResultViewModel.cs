using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsRM.Models.Results
{
    public class AddResultViewModel
    {
        public string StudentId { get; set; }
        public int Score { get; set; }
        public string SemesterId { get; set; }
        public string CourseId { get; set; }
    }
}