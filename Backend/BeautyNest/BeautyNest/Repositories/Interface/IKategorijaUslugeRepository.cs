using BeautyNest.Models.Domain;

namespace BeautyNest.Repositories.Interface
{
    public interface IKategorijaUslugeRepository
    {
        Task<KategorijaUsluge> CreateAsync(KategorijaUsluge kategorijaUsluge);
        Task <IEnumerable<KategorijaUsluge>> GetAllAsync();
        Task<KategorijaUsluge?> GetByIdAsync(int id);
    }
}
