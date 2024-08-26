using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using EcommerceProject.Repositories.Interface;

namespace EcommerceProject.Services
{
    public class ProductSizeService : IProductSizeService
    {
        private readonly IProductSizeRepository _productSizeRepository;

        public ProductSizeService(IProductSizeRepository productSizeRepository)
        {
            _productSizeRepository = productSizeRepository;
        }

        public IEnumerable<ProductSizeModel> GetSizesByProductId(int productId)
        {
            return _productSizeRepository.GetSizesByProductId(productId);
        }

        public void AddProductSize(ProductSizeModel productSize)
        {
            _productSizeRepository.Add(productSize);
        }

        public void UpdateProductSize(ProductSizeModel productSize)
        {
            _productSizeRepository.Update(productSize);
        }

        public void DeleteProductSize(int id)
        {
            _productSizeRepository.Delete(id);
        }

        public ProductSizeModel GetProductSizeById(int id)
        {
            return _productSizeRepository.GetById(id);
        }
    }
}
