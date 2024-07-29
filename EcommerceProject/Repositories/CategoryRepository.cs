using EcommerceProject.Database;
using EcommerceProject.Models;
using EcommerceProject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceProject.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            return _context.Categories
                .Include(c => c.Products) // Ensure Products are included
                .ToList() ?? new List<CategoryModel>(); // Ensure a non-null result
        }
    }

}

