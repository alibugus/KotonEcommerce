using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly UserManager<AppUser> _userManager;

        public ShopCartController(ICartService cartService, IProductService productService, UserManager<AppUser> userManager)
        {
            _cartService = cartService;
            _productService = productService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            Console.WriteLine("IsAuthenticated: " + isAuthenticated);
            var cart = _cartService.GetCart();
            CartItemViewModel cartItemViewModel = new CartItemViewModel
            {
                CartItem = cart,
                isAuthenticated = isAuthenticated
            };
            
            return View(cartItemViewModel);
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
            Console.WriteLine("Product Id: " + request.ProductId);
            _cartService.RemoveProductFromCart(request.ProductId);
            return Json(new { success = true, message = "Product removed from cart successfully!" });
        }
        [HttpPost]
        public IActionResult DecreaseProductQuantity([FromBody] ProductIdRequest request)
        {
            Console.WriteLine("Product Id: " + request.ProductId);
            _cartService.DecreaseProductQuantity(request.ProductId);
            return Json(new { success = true, message = "Product removed from cart successfully!" });
        }

        public class ProductIdRequest
        {
            public int ProductId { get; set; }
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult IncreaseProductQuantity([FromBody] ProductIdRequest request)
        {
            Console.WriteLine("Product Id: " + request.ProductId);
            _cartService.IncreaseProductQuantity(request.ProductId);
            return Json(new { success = true, message = "Product added to cart successfully!" });
        }
    }
}
