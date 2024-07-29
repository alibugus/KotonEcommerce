using EcommerceProject.Models;

namespace EcommerceProject.Repositories.Interface
{
    public interface IBrandRepository
    {
        IEnumerable<BrandModel> GetAllBrands();
    }
}
