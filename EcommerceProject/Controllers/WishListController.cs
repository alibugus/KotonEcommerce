using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using EcommerceProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceProject.Controllers
{
    [Authorize]
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
            var product = _productService.GetProductById(model.ProductId);
            if (product != null)
            {
                _wishListService.AddProductToWishList(product);
                return Json(new { success = true, message = "Product added to wishlist successfully!" });
            }

            return Json(new { success = false, message = "Product not found!" });
        }

        [HttpPost]
        public IActionResult ToggleWishList([FromBody] AddToCartRequestModel model)
        {
            var product = _productService.GetProductById(model.ProductId);
            if (product != null)
            {
                bool isInWishList = _wishListService.IsProductInWishList(model.ProductId);
                if (isInWishList)
                {
                    _wishListService.RemoveProductFromWishList(model.ProductId);
                    return Json(new { success = true, message = "Product removed from wishlist successfully!", inWishList = false });
                }
                else
                {
                    _wishListService.AddProductToWishList(product);
                    return Json(new { success = true, message = "Product added to wishlist successfully!", inWishList = true });
                }
            }

            return Json(new { success = false, message = "Product not found!" });
        }

        [HttpPost]
        public IActionResult RemoveProductFromWishList(int productId)
        {
            _wishListService.RemoveProductFromWishList(productId);
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
