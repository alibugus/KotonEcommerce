using EcommerceProject.Models;
using System.Collections.Generic;

namespace EcommerceProject.Services.Interface
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAllProducts();
        IEnumerable<ProductModel> GetFilteredProducts(List<int> categoryIds, List<int> brandIds);
        ProductModel GetProductById(int productId);
    }
}
