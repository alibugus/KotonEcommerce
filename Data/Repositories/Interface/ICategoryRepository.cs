using Data.Models;

namespace Data.Repositories.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryModel> GetAllCategories();
        CategoryModel GetCategoryById(int id);
    }
}
