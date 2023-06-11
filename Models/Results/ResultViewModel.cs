using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsRM.Models.Results
{
    public class ResultViewModel
    {
        public string StudentName { get; set; }
        public int Score { get; set; }
        public string SemesterName { get; set; }
        public string CourseName { get; set; }
    }
}