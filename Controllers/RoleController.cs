using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsRM.Models.Role;
using StudentsRM.Service.Interface;

namespace StudentsRM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly INotyfService _notyf;

        public RoleController(IRoleService roleService, INotyfService notyf)
        {
            _roleService = roleService;
            _notyf = notyf;
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

        [HttpPost]
        public IActionResult DeleteRole(string id)
        {
            var response = _roleService.Delete(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Role");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Role");
        }
    }
}