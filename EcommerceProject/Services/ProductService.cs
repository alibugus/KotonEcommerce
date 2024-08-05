using EcommerceProject.Models;
using EcommerceProject.Repositories.Interface;
using EcommerceProject.Services.Interface;
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

        public ProductModel GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }
        public IEnumerable<ProductModel> GetFilteredProducts(List<int> categoryIds, List<int> brandIds)
        {
            return _productRepository.GetFilteredProducts(categoryIds, brandIds);
        }


    }
}
