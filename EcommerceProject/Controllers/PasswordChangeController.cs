using EcommerceProject.Models;
using EcommerceProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceProject.Controllers
{
    public class PasswordChangeController : Controller
    {
        private readonly AuthService _passwordChangeService;

        public PasswordChangeController(AuthService passwordChangeService)
        {
            _passwordChangeService = passwordChangeService;
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            var passwordResetTokenLink = await _passwordChangeService.GeneratePasswordResetTokenLinkAsync(forgetPasswordViewModel.Email, Url, HttpContext.Request);
            if (passwordResetTokenLink != null)
            {
                await _passwordChangeService.SendPasswordResetEmailAsync(forgetPasswordViewModel.Email, passwordResetTokenLink);
            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var userId = TempData["userId"]?.ToString();
            var token = TempData["token"]?.ToString();
            if (userId == null || token == null)
            {
                return View();
            }

            var result = await _passwordChangeService.ResetPasswordAsync(userId, token, resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
