using EcommerceProject.Models;
using System.Collections.Generic;

namespace EcommerceProject.Services.Interface
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAllCategories();
    }
}
