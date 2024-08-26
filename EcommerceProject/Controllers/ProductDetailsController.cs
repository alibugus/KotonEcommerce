using EcommerceProject.Models;
using EcommerceProject.Services;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IModelInformationService _modelInformationService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;

        public ProductDetailsController(IProductService productService, IModelInformationService modelInformationService,IBrandService brandService,ICategoryService categoryService)
        {
            _productService = productService;
            _modelInformationService = modelInformationService;
            _brandService = brandService;
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("ProductDetails/Post")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("ProductDetails")]
        public IActionResult IndexGet(int ProductId)
        {
            var product = _productService.GetProductById(ProductId);
            var brand = _brandService.GetBrandById(product.BrandId);
            var category =_categoryService.GetCategory(product.CategoryId);
            product.Brand = brand;
            product.Category = category;
            if (product == null)
            {
                return NotFound();
            }

            var modelInformation = _modelInformationService.GetModelInformationById(product.ModelInformationId);
            var images = _productService.GetImagesByProductId(ProductId);
            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                Images = images,
                ModelInformation = modelInformation
            };

            return View("Index", viewModel);
        }
    }
}
