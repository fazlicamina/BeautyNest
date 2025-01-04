using BeautyNest.Data;
using BeautyNest.Models.Domain;
using BeautyNest.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BeautyNest.Repositories.Implementation
{
    public class UslugaRepository : IUslugaRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UslugaRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Usluga> CreateAsync(Usluga usluga)
        {
            await dbContext.Usluge.AddAsync(usluga);
            await dbContext.SaveChangesAsync();
            return usluga;
        }

        public async Task<List<Usluga>> GetByIdsAsync(List<int> ids)
        {
            return await dbContext.Usluge
                .Where(u => ids.Contains(u.Id))
                .ToListAsync();
        }


        public async Task<List<Usluga>> GetAllAsync()
        {
            return await dbContext.Usluge.Include(u => u.KategorijaUsluge).ToListAsync();
        }

        public async Task<Usluga?> GetByIdAsync(int id)
        {
            return await dbContext.Usluge.Include(u => u.KategorijaUsluge).FirstAsync(x => x.Id == id);
        }
    }
}
