using Data.Models;
using Data.Repositories.Interface;
using Data.Services.Interface;
using System.Collections.Generic;

namespace Data.Services
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
            // Assuming GetAllCategories from repository is non-null
            return _categoryRepository.GetAllCategories() ?? new List<CategoryModel>();
        }
        public CategoryModel GetCategory(int id) {
            return _categoryRepository.GetCategoryById(id);
        }
    }
}
