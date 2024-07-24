using EcommerceProject.Models;
using EcommerceProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceProject.Controllers
{
    public class ConfirmMailController : Controller
    {
        private readonly ConfirmMailService _confirmMailService;

        public ConfirmMailController(ConfirmMailService confirmMailService)
        {
            _confirmMailService = confirmMailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = TempData["Mail"];
            ViewBag.Email = value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfirmUser confirmUser)
        {
            var isConfirmed = await _confirmMailService.ConfirmUserAsync(confirmUser);
            if (isConfirmed)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
