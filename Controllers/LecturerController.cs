using StudentsRM.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using StudentsRM.Models.Lecturer;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace StudentsRM.Controllers
{
     [Authorize(Roles = "Admin")]
    public class LecturerController : Controller
    {
        private readonly ILecturerService _lecturerService;
        private readonly ICourseService _courseService;
        private readonly INotyfService _notyf;

        public LecturerController(ILecturerService lecturerService, ICourseService courseService, INotyfService notyf)
        {
            _lecturerService =  lecturerService;
            _courseService = courseService;
            _notyf = notyf;
        }
            

        public IActionResult Index()
        {
            var response = _lecturerService.GetAll();
            ViewData["Messsage"] = response.Message;
            ViewData["Status"] = response.Status;
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
        
        public IActionResult GetLecturer(string Id)
        {
            var response = _lecturerService.GetLecturer(Id);
            ViewData["Message"] = "";
            ViewData["Status"] = false;

            return View(response.Data);
        }

        public IActionResult Update(string id)
        {
            var response = _lecturerService.GetLecturer(id);
            ViewBag.Courses = _courseService.SelectCourses();

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Lecturer");
            }

            var viewModel = new UpdateLecturerViewModel
            {
                Id = response.Data.Id,
                Email = response.Data.Email,
                HomeAddress = response.Data.HomeAddress,
                PhoneNumber = response.Data.PhoneNumber,
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateLecturerViewModel request)
        {
            var response = _lecturerService.Update(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View(request);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Lecturer");
        }

        [HttpPost]
        public IActionResult DeleteLecturer(string id)
        {
            var response = _lecturerService.Delete(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Lecturer");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Lecturer");
        }
        
    }
}