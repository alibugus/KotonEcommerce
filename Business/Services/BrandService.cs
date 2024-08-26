using Data.Models;
using Data.Repositories;
using Data.Repositories.Interface;
using System.Collections.Generic;

namespace Business.Services.Interface
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IEnumerable<BrandModel> GetAllBrands()
        {
            return _brandRepository.GetAllBrands();
        }
        public BrandModel GetBrandById(int id)
        {
            return _brandRepository.GetBrandById(id);
        }
    }
}
