using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsRM.Models.Semester;
using StudentsRM.Service.Interface;

namespace StudentsRM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SemesterController : Controller
    {
        private readonly ISemesterService _semesterService;
        private readonly INotyfService _notyf;

        public SemesterController(ISemesterService semesterService, INotyfService notyf)
        {
            _semesterService = semesterService;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            var response = _semesterService.GetAll();
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(CreateSemesterViewModel reqeust)
        {
            var response = _semesterService.Create(reqeust);
            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);
            return RedirectToAction("Index", "Semester"); ;
        }

        
        [HttpPost]
        public IActionResult DeleteSemester(string id)
        {
            var response = _semesterService.Delete(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Semester");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Semester");
        }
    }
}