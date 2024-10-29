using BeautyNest.Models.Domain;

namespace BeautyNest.Repositories.Interface
{
    public interface ISalonRepository
    {
        Task<Salon> CreateAsync(Salon salon);
        Task<IEnumerable<Salon>> GetAllAsync();
        Task<Salon?> DeleteAsync (int id);
        Task<Salon?> GetByIdAsync (int id);
    }
}
