using Data.Models;

namespace Data.Services.Interface
{
    public interface IProductSizeService
    {
        IEnumerable<ProductSizeModel> GetSizesByProductId(int productId);
        void AddProductSize(ProductSizeModel productSize);
        void UpdateProductSize(ProductSizeModel productSize);
        void DeleteProductSize(int id);
        ProductSizeModel GetProductSizeById(int id);
    }
}
