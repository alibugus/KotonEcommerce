using EcommerceProject.Database;
using EcommerceProject.Models;
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
			return _context.Categories.ToList();
		}
	}
}

