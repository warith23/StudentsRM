using StudentsRM.Models.Course;
using Microsoft.AspNetCore.Mvc;
using StudentsRM.Service.Interface;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace StudentsRM.Controllers
{
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
                // _notyf.Error(response.Message);
                return View();
            }

            // _notyf.Success(response.Message);
            return RedirectToAction("Index", "Course");
        }
    }
}