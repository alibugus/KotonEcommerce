using Data.Models;
using System.Collections.Generic;

namespace Data.Services.Interface
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAllCategories();
        CategoryModel GetCategory(int id);
    }
}
