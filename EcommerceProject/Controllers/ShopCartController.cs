using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceProject.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IDiscountService _couponService;
        private readonly UserManager<AppUser> _userManager;
     

        public ShopCartController(ICartService cartService, IProductService productService, IDiscountService couponService, UserManager<AppUser> userManager)
        {
            _cartService = cartService;
            _productService = productService;
            _couponService = couponService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(CartItemViewModel cartItemViewModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Kullanıcı giriş yapmamışsa GuestShopCartController'a yönlendir.
                return RedirectToAction("Index", "GuestShopCart");
            }

            var user = await _userManager.GetUserAsync(User);
            bool isAuthenticated = User.Identity?.IsAuthenticated ?? false;
            var cart = _cartService.GetCart();

            var activeCoupons = (await _couponService.GetUserActiveCouponsAsync(user.Id)).ToList();
            var firstActiveCoupon = activeCoupons.FirstOrDefault();

            decimal discountAmount = 0;
            decimal subtotal = 0;
            decimal totalAmount = 0;

            if (firstActiveCoupon != null)
            {
                discountAmount = firstActiveCoupon.Coupon.DiscountAmount;
                subtotal = cart.Sum(item => item.Product.Price * item.Quantity);
                totalAmount = subtotal - discountAmount;
            }
            else
            {
                subtotal = cart.Sum(item => item.Product.Price * item.Quantity);
                totalAmount = subtotal;
            }

            // Retrieve errors from TempData
            var validationErrors = TempData["ValidationErrors"] as Dictionary<string, string> ?? new Dictionary<string, string>();
            var couponCode = TempData["CouponCode"] as string;

            cartItemViewModel = new CartItemViewModel
            {
                CartItem = cart,
                isAuthenticated = isAuthenticated,
                CouponCode = couponCode ?? firstActiveCoupon?.Coupon.Code,
                DiscountAmount = discountAmount,
                Subtotal = subtotal,
                TotalAmount = totalAmount,
                ValidationErrors = validationErrors
            };

            // Retrieve success message from TempData if needed
            ViewBag.SuccessMessage = TempData["SuccessMessage"];

            return View("Index", cartItemViewModel);
        }



        public async Task<IActionResult> ApplyCoupon(CartItemViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var errors = await _couponService.ValidateCouponAsync(user.Id, model.CouponCode);
            var cart = _cartService.GetCart();

            if (errors.Any())
            {
                // Store errors in TempData
                TempData["ValidationErrors"] = errors;
                
                return RedirectToAction("Index");
            }
            TempData["CouponCode"] = model.CouponCode;
            await _couponService.ApplyCouponToUserAsync(user.Id, model.CouponCode);
            var discountAmount = await _couponService.GetDiscountAmountAsync(model.CouponCode);
            var subtotal = cart.Sum(item => item.Product.Price * item.Quantity);
            var totalAmount = subtotal - discountAmount;

            var cartItemViewModel = new CartItemViewModel
            {
                CartItem = cart,
                isAuthenticated = model.isAuthenticated,
                CouponCode = model.CouponCode,
                DiscountAmount = discountAmount,
                Subtotal = subtotal,
                TotalAmount = totalAmount,
            };

            // Store success message or any relevant info in TempData if needed
            TempData["SuccessMessage"] = "Coupon applied successfully.";

            return RedirectToAction("Index", cartItemViewModel);
        }




        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(string couponCode)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return RedirectToAction("Index");
            }

            await _couponService.RemoveCouponFromUserAsync(user.Id, couponCode);

            // Deactivate the coupon
           await _couponService.DeactivateUserCouponAsync(user.Id, couponCode);

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

        public IActionResult SendConfirmation(CartItemViewModel cartItemViewModel)
        {

            var cart = _cartService.GetCart();

            cartItemViewModel = new CartItemViewModel
            {
                CartItem = cart,
                isAuthenticated = cartItemViewModel.isAuthenticated,
                CouponCode = cartItemViewModel.CouponCode,
                DiscountAmount = cartItemViewModel.DiscountAmount,
                TotalAmount = cartItemViewModel.TotalAmount
            };
            return RedirectToAction("GetCartItemViewModel", "CheckOut", cartItemViewModel);
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
    

        [HttpGet]
        public JsonResult GetCartItemCount()
        {
            var cart = _cartService.GetCart();

            int cartItemCount = cart.Sum(item => item.Quantity); // Sepetteki toplam ürün sayısı
          
            return Json(new { cartItemCount = cartItemCount });
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
