using EcommerceProject.Models;
using EcommerceProject.Services;
using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    private readonly AuthService _authService;

    public LoginController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel loginViewModel)
    {
        var result = await _authService.LoginAsync(loginViewModel);
        if (result.Succeeded)
        {
            var user = await _authService.FindByNameAsync(loginViewModel.UserName);
            if (user.EmailConfirmed)
            {
                return RedirectToAction("Index", "MyAccount");
            }
        }
        return View();
    }
}