using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly ILogger<ShopController> _logger;

        public ShopController(IProductService productService, ICategoryService categoryService, IBrandService brandService, ILogger<ShopController> logger)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            var categories = _categoryService.GetAllCategories();
            var brands = _brandService.GetAllBrands();
            var viewModel = new ShopViewModel
            {
                Products = products,
                Categories = categories,
                Brands = brands
            };
            return View(viewModel);
        }

        [HttpGet]
        public PartialViewResult GetFilteredProducts([FromQuery] List<int> categoryIds, [FromQuery] List<int> brandIds)
        {
            _logger.LogInformation("Received Category IDs: " + string.Join(", ", categoryIds ?? new List<int>()));
            _logger.LogInformation("Received Brand IDs: " + string.Join(", ", brandIds ?? new List<int>()));

            var filteredProducts = _productService.GetFilteredProducts(categoryIds, brandIds);
            var viewModel = new ShopViewModel
            {
                Products = filteredProducts
            };
            return PartialView("_ProductListPartial", viewModel);
        }
    }
}
