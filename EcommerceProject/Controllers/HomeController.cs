using EcommerceProject.Models;
using EcommerceProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EcommerceProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            var categories = _categoryService.GetAllCategories();
            var viewModel = new HomeViewModel
            {
                Products = products,
                Categories = categories
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Yeni metot: Belirli bir kategoriye göre ürünleri getirmek için
        public PartialViewResult GetProductsByCategory(int categoryId)
        {
            var products = _productService.GetProductsByCategory(categoryId);
            return PartialView("_ProductListPartial", products);
        }
    }
}
