using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
