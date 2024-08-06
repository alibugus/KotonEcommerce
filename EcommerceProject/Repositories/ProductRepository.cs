using EcommerceProject.Database;
using EcommerceProject.Models;
using EcommerceProject.Repositories.Interface;
using Microsoft.CodeAnalysis;

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
		public ProductModel GetProductById(int id) {

           return _context.Products.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<ProductModel> GetProductsByIds(IEnumerable<int> productIds)
        {             return _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
        }
        public IEnumerable<ProductModel> GetFilteredProducts(List<int> categoryIds, List<int> brandIds)
        {
            return _context.Products.Where(p =>
                (categoryIds == null || categoryIds.Count == 0 || categoryIds.Contains(p.CategoryId)) &&
                (brandIds == null || brandIds.Count == 0 || brandIds.Contains(p.BrandId))
            ).ToList();
        }
    }
}
