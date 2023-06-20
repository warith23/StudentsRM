using StudentsRM.Service.Interface;
using StudentsRM.Models.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace StudentsRM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly INotyfService _notyf;

        public StudentController(IStudentService studentService, ICourseService courseService, INotyfService notyf)
        {
            _studentService = studentService;
            _courseService = courseService;
            _notyf = notyf;
        }
        
        public IActionResult Index()
        {
            var response = _studentService.GetAll();
            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);
            return View(response.Data);
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
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);
            return RedirectToAction("Index");
        }

        public IActionResult GetStudent(string id)
        {
            var response = _studentService.GetStudent(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;
            return View(response.Data);
        }
        
        [Authorize(Roles = "Lecturer")]
        public IActionResult GetLecturerStudents()
        {
            var response = _studentService.GetAllLecturerStudents();
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;
            return View(response.Data);
        }
        
        [Authorize(Roles = "Lecturer")]
        public IActionResult GetStudentsForResults()
        {
            var response = _studentService.GetAllLecturerStudentsForResults();
            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);
            return View(response.Data);
        }
         
        [HttpPost]
        public IActionResult DeleteStudent(string id)
        {
            var response = _studentService.Delete(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Student");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Student");
        }

         public IActionResult Update(string id)
        {
            var response = _studentService.GetStudent(id);
            ViewBag.Courses = _courseService.SelectCourses();

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Student");
            }

            var viewModel = new UpdateStudentViewModel
            {
                Id = response.Data.Id,
                Email = response.Data.Email,
                HomeAddress = response.Data.HomeAddress,
                PhoneNumber = response.Data.PhoneNumber,
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateStudentViewModel request)
        {
            var response = _studentService.Update(request, id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View(request);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Student");
        }
    }
}