using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceProject.Models;
using EcommerceProject.Repositories;
using EcommerceProject.Repositories.Interface;
using EcommerceProject.Services.Interface;

namespace EcommerceProject.Services
{
    public class ModelInformationService : IModelInformationService
    {
        private readonly IModelInformationRepository _modelInformationRepository;

        public ModelInformationService(IModelInformationRepository modelInformationRepository)
        {
            _modelInformationRepository = modelInformationRepository;
        }

        public ModelInformationModel GetModelInformationById(int id)
        {
            return  _modelInformationRepository.GetById(id);
        }

        public async Task<IEnumerable<ModelInformationModel>> GetAllModelInformationAsync()
        {
            return await _modelInformationRepository.GetAllAsync();
        }

        public async Task AddModelInformationAsync(ModelInformationModel modelInformation)
        {
            await _modelInformationRepository.AddAsync(modelInformation);
        }

        public async Task UpdateModelInformationAsync(ModelInformationModel modelInformation)
        {
            await _modelInformationRepository.UpdateAsync(modelInformation);
        }

        public async Task DeleteModelInformationAsync(int id)
        {
            await _modelInformationRepository.DeleteAsync(id);
        }
    }
}
