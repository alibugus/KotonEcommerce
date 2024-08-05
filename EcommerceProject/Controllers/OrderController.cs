using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EcommerceProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(IOrderService orderService, UserManager<AppUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "GuestCheckout"); // Giriş yapmamış kullanıcıları yönlendir
            }

            var user = _userManager.GetUserAsync(User).Result;
            var orders = _orderService.GetOrdersByUserId(user.Id);
            return View(orders);
        }

   

        // Misafir siparişlerini görüntüleme (bu örnekte misafir siparişleri için bir ID alıyor)
        public IActionResult GuestOrderDetails(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
