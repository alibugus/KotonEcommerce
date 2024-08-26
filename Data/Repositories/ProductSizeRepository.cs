using Data.Database;
using Data.Models;
using Data.Repositories.Interface;

namespace Data.Repositories
{
    public class ProductSizeRepository : IProductSizeRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductSizeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductSizeModel> GetSizesByProductId(int productId)
        {
            return _context.ProductSizes.Where(ps => ps.ProductId == productId).ToList();
        }

        public void Add(ProductSizeModel productSize)
        {
            _context.ProductSizes.Add(productSize);
            _context.SaveChanges();
        }

        public void Update(ProductSizeModel productSize)
        {
            _context.ProductSizes.Update(productSize);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var productSize = _context.ProductSizes.Find(id);
            if (productSize != null)
            {
                _context.ProductSizes.Remove(productSize);
                _context.SaveChanges();
            }
        }

        public ProductSizeModel GetById(int id)
        {
            return _context.ProductSizes.Find(id);
        }


        public ProductSizeModel GetProductSizeByProductIdAndSize(int productId, string size)
        {
            return _context.ProductSizes.FirstOrDefault(ps => ps.ProductId == productId && ps.Size == size);
        }

        public void UpdateProductSize(ProductSizeModel productSize)
        {
            _context.ProductSizes.Update(productSize);
        }
    }
}
