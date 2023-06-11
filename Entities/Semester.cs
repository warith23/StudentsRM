

namespace StudentsRM.Entities
{
    public class Semester : BaseEntity
    {
        public string SemesterName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}