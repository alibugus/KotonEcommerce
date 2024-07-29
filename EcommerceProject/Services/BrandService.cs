using EcommerceProject.Models;
using EcommerceProject.Repositories;
using EcommerceProject.Repositories.Interface;
using System.Collections.Generic;

namespace EcommerceProject.Services.Interface
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
    }
}
