using Data.Models;

namespace Data.Repositories.Interface
{
    public interface IBrandRepository
    {
        IEnumerable<BrandModel> GetAllBrands();
        BrandModel GetBrandById(int id);
    }
}
