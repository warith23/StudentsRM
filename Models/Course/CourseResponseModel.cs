
namespace StudentsRM.Models.Course
{
    public class CourseResponseModel : BaseResponseModel
    {
        public CourseViewModel Data { get; set; }
    }

        public class CoursesResponseModel : BaseResponseModel
    {
        public List<CourseViewModel> Data { get; set; }
    }

}