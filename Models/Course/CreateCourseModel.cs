using System.ComponentModel.DataAnnotations;

namespace StudentsRM.Models.Course
{
    public class CreateCourseModel
    {
        [Required(ErrorMessage = "Course name is required")]
        public string Name { get; set; }
       
        // [Required(ErrorMessage = "Lecturer name is required")]
        // public string Lecturer { get; set; }
    }
}