
namespace StudentsRM.Entities
{
    public class Result : BaseEntity
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public int Score { get; set; }
        public string SemesterId { get; set; }
        public Semester Semester { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}