using StudentsRM.Models;
using StudentsRM.Models.Course;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentsRM.Service.Interface
{
    public interface ICourseService
    {
        BaseResponseModel Create(CreateCourseModel request);
        BaseResponseModel Delete(string courseId);
        BaseResponseModel Update(string courseId, UpdateCourseViewModel update);
        CourseResponseModel GetCourse(string courseId);
        CoursesResponseModel GetAll();
        IEnumerable<SelectListItem> SelectCourses();
    }
}