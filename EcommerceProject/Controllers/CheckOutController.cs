using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceProject.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAddressService _addressService;

        public CheckoutController(ICartService cartService, IOrderService orderService, UserManager<AppUser> userManager, IAddressService addressService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
            _addressService = addressService;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "GuestCheckout");
            }

            var user = await _userManager.GetUserAsync(User);
            var cart = _cartService.GetCart();
            var addresses = _addressService.GetAddressesByUserId(user.Id);

            if (cart == null || !cart.Any())
            {
                ModelState.AddModelError("", "Your cart is empty.");
                return RedirectToAction("Index", "Shop");
            }

            var model = new CheckOutViewModel
            {
                CartItems = cart,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                City = user.City,
                SavedAddresses = addresses,
                TotalAmount = Convert.ToDecimal(ViewData["Deger"])
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckOutViewModel model)
        {
            model.CartItems = _cartService.GetCart();
            var user = await _userManager.GetUserAsync(User);
            AddressModel selectedAddress;

            if (model.SelectedAddressId != 0)
            {
                selectedAddress = _addressService.GetAddressById(model.SelectedAddressId);
            }
            else
            {
                selectedAddress = new AddressModel
                {
                    UserId = user.Id,
                    Country = model.Country,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    User = user
                };

                if (string.IsNullOrEmpty(selectedAddress.Address))
                {
                    ModelState.AddModelError("", "Address cannot be empty.");
                    return View("Index", model);
                }

                _addressService.AddAddress(selectedAddress);
            }

            var order = new OrderModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = selectedAddress.Country,
                Address = selectedAddress.Address,
                City = selectedAddress.City,
                State = selectedAddress.State,
                ZipCode = selectedAddress.ZipCode,
                Phone = user.PhoneNumber,
                Email = user.Email,
                OrderNotes = model.OrderNotes,
                UserId = user.Id
            };

            _orderService.PlaceOrder(order, model.CartItems);

            var savedOrder = _orderService.GetOrdersByUserId(order.UserId)
                                          .OrderByDescending(o => o.Id)
                                          .FirstOrDefault();
            decimal totalAmount = Convert.ToDecimal(TempData["TotalAmount"]);
            if (savedOrder != null)
            {
                foreach (var cartItem in model.CartItems)
                {
                    var orderDetail = new OrderDetailModel
                    {
                        OrderId = savedOrder.Id,
                        ProductId = cartItem.Product.Id,
                        Quantity = cartItem.Quantity,
                        Price = totalAmount
                    };
                    _orderService.AddOrderDetail(orderDetail);
                }

                _cartService.ClearCart();
            }

            return RedirectToAction("Index", "Order");
        }
    }
}
