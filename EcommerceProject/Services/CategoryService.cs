using EcommerceProject.Models;
using EcommerceProject.Repositories;
using System.Collections.Generic;

namespace EcommerceProject.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public IEnumerable<CategoryModel> GetAllCategories()
		{
			return _categoryRepository.GetAllCategories();
		}
	}
}
