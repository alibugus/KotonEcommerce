using EcommerceProject.Models;

namespace EcommerceProject.Services.Interface
{
    public interface IBrandService
    {
        IEnumerable<BrandModel> GetAllBrands();
    }
}
