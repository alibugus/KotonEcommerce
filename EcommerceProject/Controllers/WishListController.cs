using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using EcommerceProject.Models;
using Microsoft.CodeAnalysis;

namespace EcommerceProject.Controllers
{
    public class WishListController : Controller
    {
        private readonly IWishListService _wishListService;
        private readonly IProductService _productService;

        public WishListController(IWishListService wishListService, IProductService productService)
        {
            _wishListService = wishListService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var wishlist = _wishListService.GetWishList();
            return View(wishlist);
        }

        [HttpPost]
        public IActionResult AddToWishList([FromBody] AddToCartRequestModel model)
        {
            Console.WriteLine("Received ProductId: " + model.ProductId);
            var product = _productService.GetProductById(model.ProductId);
            if (product != null)
            {
                _wishListService.AddProductToWishList(product);
                return Json(new { success = true, message = "Product added to wishlist successfully!" });
            }

            return Json(new { success = false, message = "Product not found!" });
        }

        [HttpPost]
        public IActionResult RemoveProductFromWishList(int ProductId)
        {
            var a = ProductId;
            Console.WriteLine("Received ProductId: " + ProductId);
            _wishListService.RemoveProductFromWishList(ProductId);
            return Json(new { success = true, message = "Product removed from wishlist successfully!" });
        }

        [HttpPost]
        public JsonResult IsProductInWishList(int productId)
        {
            var isInWishList = _wishListService.IsProductInWishList(productId);
            return Json(new { inWishList = isInWishList });
        }
  
    }
}
