using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsRM.Models.Results;
using StudentsRM.Service.Interface;

namespace StudentsRM.Controllers
{
    [Authorize]
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;
        private readonly ICourseService _courseService;
        private readonly INotyfService _notyf;

        public ResultController(IResultService resultService, ICourseService courseService, INotyfService notyf)
        {
            _resultService =  resultService;
            _courseService = courseService;
            _notyf = notyf;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

       
        [Authorize(Roles = "Lecturer")]
        public IActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        public IActionResult Create(AddResultViewModel request, string id)
        {
            var response = _resultService.Create(request, id);
            if (response.Status == false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);
            return RedirectToAction("GetStudentsForResults", "Student");
        }

        [Authorize(Roles = "Student")]
        public IActionResult CheckResult()
        {
            var response = _resultService.CheckStudentResult();
            if(response.Status == false)
            {
                _notyf.Error(response.Message);
                return View();
            }
             _notyf.Success(response.Message);   
            return View(response.Data);
        }

        
    }
}

