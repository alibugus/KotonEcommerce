using Data.Database;
using Data.Models;
using Data.Repositories.Interface;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
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
            return _context.Products
                           .Include(p => p.ProductSizes)  // Include ProductSizes
                           .Include(p => p.Category)
                           .Include(p => p.Brand)
                           .ToList();
        }
        public ProductModel GetProductById(int id)
        {
            return _context.Products
                           .Include(p => p.ProductSizes)
                           .Include(p => p.Category)
                           .Include(p => p.Brand)
                           .FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<ProductModel> GetProductsByIds(IEnumerable<int> productIds)
        {             return _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
        }
        public IEnumerable<ProductModel> GetFilteredProducts(List<int> categoryIds, List<int> brandIds)
        {
            return _context.Products.Where(p =>
                (categoryIds == null || categoryIds.Count == 0 || categoryIds.Contains(p.CategoryId)) &&
                (brandIds == null || brandIds.Count == 0 || brandIds.Contains(p.BrandId))
            ).Include(p => p.ProductSizes) // ProductSizes'i dahil ediyoruz
        .ToList();
        }
        
        Task IProductRepository.UpdateProduct(ProductModel product)
        {
            return Task.Run(() =>
            {
                _context.Products.Update(product);
                _context.SaveChanges();
            });
        }

        public IEnumerable<ProductImageModel> GetImagesByProductId(int productId)
        {
            return _context.ProductImages
                           .Where(pi => pi.ProductId == productId)
                           .ToList();
        }

        public int GetTotalStockQuantity(int productId)
        {
            return _context.ProductSizes
                           .Where(ps => ps.ProductId == productId)
                           .Sum(ps => ps.StockQuantity);
        }
    }
}
