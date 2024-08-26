using Data.Models;
namespace Data.Repositories.Interface
{
    public interface IProductRepository
    {
        

        IEnumerable<ProductModel> GetAllProducts();
        IEnumerable<ProductModel> GetFilteredProducts(List<int> categoryIds, List<int> brandIds);
        ProductModel GetProductById(int productId);
        int GetTotalStockQuantity(int productId);
        Task UpdateProduct(ProductModel product);
        IEnumerable<ProductModel> GetProductsByIds(IEnumerable<int> productIds);
        IEnumerable<ProductImageModel> GetImagesByProductId(int productId);
    }
}
