using EcommerceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceProject.Controllers
{
    public class ConfirMailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirMailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var value = TempData["Mail"];
            ViewBag.Email = value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfirmUser a)
        {
            var user =await _userManager.FindByEmailAsync(a.Mail);
            if (user.ConfirmCode == a.ConfirmCode)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user); // Kullanıcıyı güncelle
                return RedirectToAction("Index", "Login"); // Onaylandıktan sonra yönlendirme
            }
            return View();
        }

       
    }
}
