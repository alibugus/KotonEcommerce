using EcommerceProject.Models;
namespace EcommerceProject.Repositories.Interface
{
    public interface IProductRepository
    {
        

        IEnumerable<ProductModel> GetAllProducts();
        IEnumerable<ProductModel> GetFilteredProducts(List<int> categoryIds, List<int> brandIds);
        ProductModel GetProductById(int productId);
        IEnumerable<ProductModel> GetProductsByIds(IEnumerable<int> productIds);
    }
}
