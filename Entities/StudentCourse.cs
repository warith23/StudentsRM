namespace StudentsRM.Entities
{
    public class StudentCourse : BaseEntity
    {
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}