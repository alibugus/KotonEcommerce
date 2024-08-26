using Data.Models;

namespace Data.Services.Interface
{
    public interface IModelInformationService
    {
        ModelInformationModel GetModelInformationById(int id);
        Task<IEnumerable<ModelInformationModel>> GetAllModelInformationAsync();
        Task AddModelInformationAsync(ModelInformationModel modelInformation);
        Task UpdateModelInformationAsync(ModelInformationModel modelInformation);
        Task DeleteModelInformationAsync(int id);
    }
}
