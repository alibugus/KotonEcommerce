using EcommerceProject.Models;
namespace EcommerceProject.Repositories.Interface
{
    public interface IProductRepository
    {
        //IEnumerable<ProductModel> GetAllProducts();

        IEnumerable<ProductModel> GetAllProducts();
        IEnumerable<ProductModel> GetFilteredProducts(List<int> categoryIds, List<int> brandIds);

    }
}
