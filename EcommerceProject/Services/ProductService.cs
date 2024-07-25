using EcommerceProject.Models;
using EcommerceProject.Repositories;
using System.Collections.Generic;

namespace EcommerceProject.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public IEnumerable<ProductModel> GetProductsByCategory(int categoryId)
        {
            return _productRepository.GetProductsByCategory(categoryId);
        }
    }
}
