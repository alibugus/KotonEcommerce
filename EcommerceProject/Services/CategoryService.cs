using EcommerceProject.Models;
using EcommerceProject.Repositories.Interface;
using EcommerceProject.Services.Interface;
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
            // Assuming GetAllCategories from repository is non-null
            return _categoryRepository.GetAllCategories() ?? new List<CategoryModel>();
        }
    }
}
