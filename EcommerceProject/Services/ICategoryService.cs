using EcommerceProject.Models;
using System.Collections.Generic;

namespace EcommerceProject.Services
{
	public interface ICategoryService
	{
		IEnumerable<CategoryModel> GetAllCategories();
	}
}
