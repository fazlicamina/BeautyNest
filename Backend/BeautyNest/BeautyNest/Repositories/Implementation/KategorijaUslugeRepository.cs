using BeautyNest.Data;
using BeautyNest.Models.Domain;
using BeautyNest.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BeautyNest.Repositories.Implementation
{
    public class KategorijaUslugeRepository : IKategorijaUslugeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public KategorijaUslugeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<KategorijaUsluge> CreateAsync(KategorijaUsluge kategorijaUsluge)
        {
           await dbContext.KategorijeUsluga.AddAsync(kategorijaUsluge);
           await dbContext.SaveChangesAsync();
           return kategorijaUsluge;
        }

        public async Task<IEnumerable<KategorijaUsluge>> GetAllAsync()
        {
            return await dbContext.KategorijeUsluga.Include(k => k.Usluge).ToListAsync();
        }

        public async Task<KategorijaUsluge?> GetByIdAsync(int id)
        {
            return await dbContext.KategorijeUsluga.Include(k => k.Usluge).FirstAsync(x => x.Id == id);
        }
    }
}
