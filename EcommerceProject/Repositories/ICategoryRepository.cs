using EcommerceProject.Models;

namespace EcommerceProject.Repositories
{
	public interface ICategoryRepository
	{
		IEnumerable<CategoryModel> GetAllCategories();
	}
}
