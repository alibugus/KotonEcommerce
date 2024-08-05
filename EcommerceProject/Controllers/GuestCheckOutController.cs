using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EcommerceProject.Controllers
{
    public class GuestCheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public GuestCheckoutController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            var model = new GuestCheckoutViewModel
            {
                CartItems = cart,
                Order = new OrderModel()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult PlaceOrder(GuestCheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cart = _cartService.GetCart();

                var order = new OrderModel
                {
                    FirstName = model.Order.FirstName,
                    LastName = model.Order.LastName,
                    Email = model.Order.Email,
                    Address = model.Order.Address,
                    City = model.Order.City,
                    State = model.Order.State,
                    ZipCode = model.Order.ZipCode,
                    Phone = model.Order.Phone,
                    OrderNotes = model.Order.OrderNotes,
                    OrderDate = DateTime.Now,
                    OrderDetails = cart.Select(item => new OrderDetailModel
                    {
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        Price = item.Product.Price
                    }).ToList()
                };

                _orderService.PlaceOrder(order, cart);

                // Clear the cart
                _cartService.ClearCart();

                return RedirectToAction("OrderConfirmation");
            }

            model.CartItems = _cartService.GetCart();
            return View("Index", model);
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
