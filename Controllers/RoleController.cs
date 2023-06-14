using Microsoft.AspNetCore.Mvc;
using StudentsRM.Models.Role;
using StudentsRM.Service.Interface;

namespace StudentsRM.Controllers
{
    // [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            var response = _roleService.GetAll();
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(CreateRoleModel reqeust)
        {
            var response = _roleService.Create(reqeust);
            if (response.Status is false)
            {
                //_notyf.Error(response.Message);
                ViewData["Message"] = response.Message;
                return View();
            }

            ViewData["Message"] = response.Message;
            return RedirectToAction("Index", "role"); 
        }
    }
}