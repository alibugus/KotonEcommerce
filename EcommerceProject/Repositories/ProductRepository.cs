using EcommerceProject.Database;
using EcommerceProject.Models;

namespace EcommerceProject.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _context;

		public ProductRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<ProductModel> GetAllProducts()
		{
			return _context.Products.ToList();
		}
        public IEnumerable<ProductModel> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
        }

    }
}
