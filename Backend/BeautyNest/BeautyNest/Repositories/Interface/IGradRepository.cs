using BeautyNest.Models.Domain;

namespace BeautyNest.Repositories.Interface
{
    public interface IGradRepository
    {
        Task<IEnumerable<Grad>> GetAllAsync();
    }
}
