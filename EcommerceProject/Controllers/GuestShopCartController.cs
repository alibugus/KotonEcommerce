using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class GuestShopCartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IDiscountService _couponService;
        private readonly IGuestCouponService _guestCouponService;
       
        public GuestShopCartController(ICartService cartService, IProductService productService, IDiscountService couponService, IGuestCouponService guestCouponService)
        {
            _cartService = cartService;
            _productService = productService;
            _couponService = couponService;
            _guestCouponService = guestCouponService;
        }

        public async Task<IActionResult> Index(CartItemViewModel cartItemViewModel)
        {
            var cart = _cartService.GetCart();
            cartItemViewModel.CartItem = cart;

            var guestId = _guestCouponService.GetGuestId();
            var validationErrors = TempData["ValidationErrors"] as Dictionary<string, string> ?? new Dictionary<string, string>();
            var activeCoupons = await _guestCouponService.GetActiveCoupon(guestId);
            if (activeCoupons != null)
            {
                cartItemViewModel.CouponCode = activeCoupons.Code;
            }
            

            var model = await _guestCouponService.SetAmount(cartItemViewModel);
            model.ValidationErrors = validationErrors;
            
            return View(model);
        }
        

        public async Task<IActionResult> ApplyCoupon(CartItemViewModel cartItemViewModel)
        {
            var coupon = await _guestCouponService.GetCouponByCode(cartItemViewModel.CouponCode);
            var GuestId = _guestCouponService.GetGuestId();
            if (coupon == null)
            {
                return Json(new { success = false, message = "Coupon not found!" });
            }
            var errors = await _guestCouponService.ValidateCouponAsync(GuestId, cartItemViewModel.CouponCode);
            var cart = _cartService.GetCart();

            if (errors.Any())
            {
                // Store errors in TempData
                TempData["ValidationErrors"] = errors;

                return RedirectToAction("Index");
            }

            var isValid = await _guestCouponService.ValidateCoupon(cartItemViewModel.CouponCode);
            if (!isValid)
            {
                return Json(new { success = false, message = "Coupon is not valid!" });
            }

            
           var guestId = _guestCouponService.GetGuestId();
        

           var model = await _guestCouponService.ApplyCouponForGuest(guestId, cartItemViewModel);
           await _guestCouponService.ActivateCoupon(guestId, cartItemViewModel.CouponCode);
            return RedirectToAction("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(string couponCode)
        {
       

             await _guestCouponService.DeactivateCoupon(_guestCouponService.GetGuestId(), couponCode);


            var cart = _cartService.GetCart();
            var totalAmount = cart.Sum(item => item.Product.Price * item.Quantity);

            var cartItemViewModel = new CartItemViewModel
            {
                CartItem = cart,
                isAuthenticated = User.Identity.IsAuthenticated,
                CouponCode = null,
                DiscountAmount = 0,
                Subtotal = totalAmount,
                TotalAmount = totalAmount
            };

            return RedirectToAction("Index", cartItemViewModel);
        }
        [HttpPost]
        public IActionResult AddToCart([FromBody] AddToCartRequestModel model)
        {
            var product = _productService.GetProductById(model.ProductId);
            if (product != null)
            {
                _cartService.AddProductToCart(product, model.Quantity,model.SelectedSize);
                return Json(new { success = true, message = "Product added to cart successfully!" });
            }

            return Json(new { success = false, message = "Product not found!" });
        }

        [HttpPost]
        public IActionResult RemoveProductFromCart([FromBody] ProductIdRequest request)
        {
            _cartService.RemoveProductFromCart(request.ProductId);
            return Json(new { success = true, message = "Product removed from cart successfully!" });
        }

        [HttpPost]
        public IActionResult DecreaseProductQuantity([FromBody] ProductIdRequest request)
        {
            _cartService.DecreaseProductQuantity(request.ProductId);
            return Json(new { success = true, message = "Product quantity decreased successfully!" });
        }

        [HttpPost]
        public IActionResult IncreaseProductQuantity([FromBody] ProductIdRequest request)
        {
            _cartService.IncreaseProductQuantity(request.ProductId);
            return Json(new { success = true, message = "Product quantity increased successfully!" });
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }

        public class ProductIdRequest
        {
            public int ProductId { get; set; }
        }

        public class AddToCartRequestModel
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public string SelectedSize { get; set; }
        }

    }
}
