using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            bool isAuthenticated = User.Identity.IsAuthenticated;
            var cart = _cartService.GetCart();
        

            cartItemViewModel = new CartItemViewModel
            {
                CartItem = cart,
                isAuthenticated = isAuthenticated,
                CouponCode = cartItemViewModel.CouponCode,
                DiscountAmount = cartItemViewModel.DiscountAmount,
                TotalAmount = cart.Sum(item => item.Product.Price * item.Quantity) - cartItemViewModel.DiscountAmount
            };

            return View(cartItemViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CouponRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var isValid = await _couponService.ValidateCouponCodeAsync(model.CouponCode);
                if (!isValid)
                {
                    ModelState.AddModelError(string.Empty, "Invalid coupon code.");
                    return RedirectToAction("Index");
                }

                var discountAmount = await _couponService.GetDiscountAmountAsync(model.CouponCode);
                var cart = _cartService.GetCart();
                var totalAmount = cart.Sum(item => item.Product.Price * item.Quantity) - discountAmount;

                var cartItemViewModel = new CartItemViewModel
                {
                    CartItem = cart,
                    isAuthenticated = User.Identity.IsAuthenticated,
                    CouponCode = model.CouponCode,
                    DiscountAmount = discountAmount,
                    TotalAmount = totalAmount
                };

                return RedirectToAction("Index",cartItemViewModel);
            }

            ModelState.AddModelError(string.Empty, "Error applying coupon.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] AddToCartRequestModel model)
        {
            var product = _productService.GetProductById(model.ProductId);
            if (product != null)
            {
                _cartService.AddProductToCart(product, model.Quantity);
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
        }
        public class CouponRequestModel
        {
            public string CouponCode { get; set; }
        }
    }
}
