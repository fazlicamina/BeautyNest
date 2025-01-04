using BeautyNest.Models.Domain;

namespace BeautyNest.Repositories.Interface
{
    public interface IUslugaRepository
    {
        Task<Usluga> CreateAsync(Usluga usluga);
        Task<Usluga?> GetByIdAsync(int id);
        Task<List<Usluga>> GetByIdsAsync(List<int> ids);

        Task<List<Usluga>> GetAllAsync();
    }
}
