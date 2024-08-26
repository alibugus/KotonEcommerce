using EcommerceProject.Models;

namespace EcommerceProject.Repositories.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryModel> GetAllCategories();
        CategoryModel GetCategoryById(int id);
    }
}
