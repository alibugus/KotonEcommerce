using EcommerceProject.Models;
using System.Collections.Generic;

namespace EcommerceProject.Services
{
	public interface IProductService
	{
		IEnumerable<ProductModel> GetAllProducts();
        IEnumerable<ProductModel> GetProductsByCategory(int categoryId);
    }
}
