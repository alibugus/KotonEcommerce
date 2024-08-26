using Data.Models;

namespace Data.Services.Interface
{
    public interface IBrandService
    {
        IEnumerable<BrandModel> GetAllBrands();
        BrandModel GetBrandById(int id);
    }
}
