using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using StudentsRM.Models;
using StudentsRM.Models.Auth;
using StudentsRM.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using StudentsRM.Models.User;

namespace StudentsRM.Controllers;

public class HomeController : Controller
{
    private readonly IUserService _userService;
    private readonly INotyfService _notyf;
    
    public HomeController(IUserService userService, INotyfService notyf)
    {
        _userService = userService;
        _notyf = notyf;
    }
    
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    // [RedirectIfAuthenticated]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        var response = _userService.Login(model);
        var user = response.Data;

        if (response.Status == false)
        { 
            _notyf.Error(response.Message);

            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.GivenName, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, user.RoleName),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authenticationProperties = new AuthenticationProperties();

        var principal = new ClaimsPrincipal(claimsIdentity);

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

        _notyf.Success(response.Message);

        // if (user.RoleName == "Admin")
        // {
        //     return RedirectToAction("AdminDashboard", "Home");
        // }

        if (user.RoleName == "Student")
        {
            return RedirectToAction("GetStudent", "Student", response.Data.CheckUserId);
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult LogOut()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        _notyf.Success("You have successfully signed out!");
        return RedirectToAction("Login", "Home");
    }
    
    [Authorize(Roles = "Admin")]
    public IActionResult AdminDashboard()
    {
        return View();
    }

    public IActionResult UpdatePassword()
    {
        return View();
    }
    [HttpPost]
    public IActionResult UpdatePassword(UpdateUserViewModel request)
    {
        var response = _userService.UpdatePassword(request);
        if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);
            return RedirectToAction("Index", "Home");
    }

}
