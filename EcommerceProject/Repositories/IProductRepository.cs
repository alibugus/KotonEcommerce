using EcommerceProject.Models;
namespace EcommerceProject.Repositories
{
	public interface IProductRepository
	{
		IEnumerable<ProductModel> GetAllProducts();
		IEnumerable<ProductModel> GetProductsByCategory(int categoryId);

	}
}
