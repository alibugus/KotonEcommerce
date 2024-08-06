using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


namespace EcommerceProject.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;

        public OrderDetailController(IOrderService orderService, UserManager<AppUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("OrderDetail/Post")]
        public IActionResult Index(int OrderId)
        {
            var order = _orderService.GetOrderById(OrderId);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpGet]
        [Route("OrderDetail")]
        public IActionResult IndexGet(int OrderId)
        {
            var order = _orderService.GetOrderById(OrderId);
            var products = _orderService.GetOrderProducts(OrderId);
            var orderdetailviewmodel = new OrderDetailViewModel
            {
                Order = order,
                Product = products
            };
            if (order == null)
            {
                return NotFound();
            }
            return View("Index", orderdetailviewmodel);
        }
    }
}

