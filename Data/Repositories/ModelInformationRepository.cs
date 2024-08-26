using Microsoft.EntityFrameworkCore;
using Data.Database;
using Data.Models;
using Data.Repositories.Interface;

namespace Data.Repositories
{
    public class ModelInformationRepository : IModelInformationRepository
    {
        private readonly ApplicationDbContext _context;

        public ModelInformationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ModelInformationModel GetById(int id)
        {
            return _context.ModelInformationModels
                   .FirstOrDefault(m => m.Id == id);
        }

        public async Task<IEnumerable<ModelInformationModel>> GetAllAsync()
        {
            return await _context.ModelInformationModels.ToListAsync();
        }

        public async Task AddAsync(ModelInformationModel modelInformation)
        {
            _context.ModelInformationModels.Add(modelInformation);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ModelInformationModel modelInformation)
        {
            _context.ModelInformationModels.Update(modelInformation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var modelInformation =  GetById(id);
            if (modelInformation != null)
            {
                _context.ModelInformationModels.Remove(modelInformation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
