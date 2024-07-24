using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class MyAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
