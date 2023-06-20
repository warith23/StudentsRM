using StudentsRM.Models.Course;
using Microsoft.AspNetCore.Mvc;
using StudentsRM.Service.Interface;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace StudentsRM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        public readonly INotyfService _notyf;
        public CourseController(ICourseService courseService, INotyfService notyf)
        {
            _courseService = courseService;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            var course = _courseService.GetAll();
            ViewData["Message"] = course.Message;
            ViewData["Status"] = course.Status;
            return View(course.Data);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(CreateCourseModel request)
        {
            var response = _courseService.Create(request);
            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);
            return RedirectToAction("Index", "Course");
        }

         [HttpPost]
        public IActionResult Delete(string id)
        {
            var response = _courseService.Delete(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Course");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Course");
        }
    }
}