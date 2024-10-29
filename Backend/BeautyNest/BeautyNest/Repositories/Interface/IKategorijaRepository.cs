using BeautyNest.Models.Domain;

namespace BeautyNest.Repositories.Interface
{
    public interface IKategorijaRepository
    {
        Task<IEnumerable<Kategorija>> GetAllAsync();
        Task<Kategorija?> GetById(int id);
    }
}
