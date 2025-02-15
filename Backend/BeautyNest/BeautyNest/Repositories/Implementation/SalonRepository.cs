using BeautyNest.Data;
using BeautyNest.Models.Domain;
using BeautyNest.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BeautyNest.Repositories.Implementation
{
    public class SalonRepository : ISalonRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SalonRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Salon> CreateAsync(Salon salon)
        {
            await dbContext.AddAsync(salon);
            await dbContext.SaveChangesAsync();
            return salon;
        }

        public async Task<Salon?> DeleteAsync(int id)
        {
            var existingSalon = await dbContext.Saloni.FirstOrDefaultAsync(x => x.Id == id); 
            if (existingSalon == null) { return null; }
            dbContext.Saloni.Remove(existingSalon);
            await dbContext.SaveChangesAsync();
            return existingSalon;
        }

        public async Task<IEnumerable<Salon>> GetAllAsync()
        {
           return await dbContext.Saloni.Include(x=>x.Kategorije).Include(x=>x.KategorijeUsluga).Include(x=>x.Grad).Include(s => s.GalerijaSlika).ToListAsync();
        }

        public async Task<Salon?> GetByIdAsync(int id)
        {
            return await dbContext.Saloni.Include(x => x.Kategorije).Include(s => s.GalerijaSlika).Include(x => x.KategorijeUsluga).Include(x => x.Grad).FirstAsync(x=>x.Id== id);
        }


      


    }


}
