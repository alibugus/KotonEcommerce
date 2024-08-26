using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
        private readonly IDiscountService _discountService;

        public CheckoutController(ICartService cartService, IOrderService orderService, UserManager<AppUser> userManager, IAddressService addressService, IDiscountService discountService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
            _addressService = addressService;
            _discountService = discountService;
        }

        public async Task<IActionResult> Index(CheckOutViewModel checkOutViewModel)
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "GuestCheckout");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "GuestCheckout");
            }

            var cart = _cartService.GetCart();
            var addresses = _addressService.GetAddressesByUserId(user.Id);
            var activeCoupons = (await _discountService.GetUserActiveCouponsAsync(user.Id)).ToList();
            var firstActiveCoupon = activeCoupons.FirstOrDefault();
            decimal discountamount = 0;
            if (firstActiveCoupon != null)
            {
                discountamount = firstActiveCoupon.Coupon.DiscountAmount;
            }
            var model = new CheckOutViewModel
            {
                CartItems = cart,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                City = user.City,
                SavedAddresses = addresses,
                CouponCode = checkOutViewModel.CouponCode,
                TotalAmount = cart.Sum(item => item.Product.Price * item.Quantity) - discountamount
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItemViewModel(CartItemViewModel cartItemViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            bool isAuthenticated = User.Identity?.IsAuthenticated ?? false;
            var cart = _cartService.GetCart();

            var model = new CartItemViewModel
            {
                CartItem = cart,
                isAuthenticated = isAuthenticated,
                TotalAmount = cartItemViewModel.TotalAmount,
                CouponCode = cartItemViewModel.CouponCode,
                DiscountAmount = cartItemViewModel.DiscountAmount,
            };
           

            return RedirectToAction("Index", "Checkout", model);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckOutViewModel model)
        {
            var cart = _cartService.GetCart();
            model.CartItems = cart;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "GuestCheckout");
            }
            var activeCoupons = (await _discountService.GetUserActiveCouponsAsync(user.Id)).ToList();
            var firstActiveCoupon = activeCoupons.FirstOrDefault();
            decimal  discountamount = 0;
            if (firstActiveCoupon != null)
            {
                discountamount = firstActiveCoupon.Coupon.DiscountAmount;
            }
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
                UserId = user.Id,

                TotalPrice = cart.Sum(item => item.Product.Price * item.Quantity) - discountamount
            };

            _orderService.PlaceOrder(order, model.CartItems);

             
                 await _discountService.MarkCouponAsUsedAsync(user.Id, model.CouponCode);
       
            

            return RedirectToAction("Index", "Order");
        }
    }
}
