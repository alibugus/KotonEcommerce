using Data.Database;
using Data.Models;
using Data.Repositories.Interface;

namespace Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BrandModel> GetAllBrands()
        {
            return _context.Brands.ToList();
        }
        public BrandModel GetBrandById(int id)
        {
            return _context.Brands.Find(id);
        }
    }
}
