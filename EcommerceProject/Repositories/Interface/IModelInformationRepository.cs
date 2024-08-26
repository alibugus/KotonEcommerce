using EcommerceProject.Models;

namespace EcommerceProject.Repositories.Interface
{
    public interface IModelInformationRepository
    {
        ModelInformationModel GetById(int id);
        Task<IEnumerable<ModelInformationModel>> GetAllAsync();
        Task AddAsync(ModelInformationModel modelInformation);
        Task UpdateAsync(ModelInformationModel modelInformation);
        Task DeleteAsync(int id);
    }
}
