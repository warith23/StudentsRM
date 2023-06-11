

namespace StudentsRM.Models.Lecturer
{
    public class CreateLecturerModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        // public List<string> CourseId { get; set; }
        public string CourseId { get; set; }
    }
}