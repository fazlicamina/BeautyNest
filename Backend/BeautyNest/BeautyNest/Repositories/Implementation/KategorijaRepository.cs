using BeautyNest.Data;
using BeautyNest.Models.Domain;
using BeautyNest.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BeautyNest.Repositories.Implementation
{
    public class KategorijaRepository : IKategorijaRepository
    {
        private readonly ApplicationDbContext dbContext;

        public KategorijaRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Kategorija>> GetAllAsync()
        {
            return await dbContext.Kategorije.ToListAsync();
        }

        public async Task<Kategorija?> GetById(int id)
        {
            return await dbContext.Kategorije.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
