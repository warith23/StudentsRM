using StudentsRM.Service.Interface;
using StudentsRM.Models.Student;
using Microsoft.AspNetCore.Mvc;

namespace StudentsRM.Controllers
{
    // [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public StudentController(IStudentService studentService, ICourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            var response = _studentService.GetAll();
            ViewData["Message"] = response.Message;
            ViewData["Data"] = response.Data;
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Courses = _courseService.SelectCourses();
            ViewData["Message"] = "";
            ViewData["Status"] = false;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateStudentModel request)
        {
            var response = _studentService.Create(request);
            if (response.Status is false)
            {
                ViewData["Message"] = response.Message;
                return View();
            }

            ViewData["Message"] = response.Message;
            return RedirectToAction("Index");
        }
    }
}