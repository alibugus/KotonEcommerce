using EcommerceProject.Models;
using EcommerceProject.Models.Dto;
using EcommerceProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcommerceProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AuthService _authService;

        public RegisterController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return View(appUserRegisterDto);
            }

            try
            {
                var result = await _authService.RegisterUserAsync(appUserRegisterDto);
                if (result.Succeeded)
                {
                    TempData["Mail"] = appUserRegisterDto.Email;
                    return RedirectToAction("Index", "ConfirmMail");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        Console.WriteLine($"Error: {error.Description}");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return View(appUserRegisterDto);
        }
    }
}
