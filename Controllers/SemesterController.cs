using Microsoft.AspNetCore.Mvc;
using StudentsRM.Models.Semester;
using StudentsRM.Service.Interface;

namespace StudentsRM.Controllers
{
    // [Route("[controller]")]
    public class SemesterController : Controller
    {
        private readonly ISemesterService _semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
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
                //_notyf.Error(response.Message);
                ViewData["Message"] = response.Message;
                return View();
            }

            ViewData["Message"] = response.Message;
            return RedirectToAction("Index", "Semester"); ;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}