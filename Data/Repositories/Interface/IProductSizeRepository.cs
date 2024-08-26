using Data.Models;

namespace Data.Repositories.Interface
{
    public interface IProductSizeRepository
    {
        IEnumerable<ProductSizeModel> GetSizesByProductId(int productId);
        void Add(ProductSizeModel productSize);
        void Update(ProductSizeModel productSize);
        ProductSizeModel GetProductSizeByProductIdAndSize(int productId, string size);
        void Delete(int id);
        void UpdateProductSize(ProductSizeModel productSize);
        ProductSizeModel GetById(int id);
        
    }
}
