using StudentsRM.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using StudentsRM.Models.Lecturer;

namespace StudentsRM.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ILecturerService _lecturerService;
        private readonly ICourseService _courseService;

        public LecturerController(ILecturerService lecturerService, ICourseService courseService)
        {
            _lecturerService =  lecturerService;
            _courseService = courseService;
        }
            

        public IActionResult Index()
        {
            var response = _lecturerService.GetAll();
            ViewData["Messsage"] = response.Message;
            ViewData["Status"] = response.Status;
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
        public IActionResult Create(CreateLecturerModel request)
        {
            var response = _lecturerService.Create(request);
            if (response.Status == false)
            {
                ViewData["Message"] = response.Message;
                return View();
            }

            ViewData["Message"] = response.Message;
            return RedirectToAction("Index");
        }
    }
}