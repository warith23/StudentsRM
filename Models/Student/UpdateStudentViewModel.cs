using StudentsRM.Models.Course;

namespace StudentsRM.Models.Student
{
    public class UpdateStudentViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateAdmitted { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}