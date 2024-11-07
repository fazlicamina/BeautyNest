using BeautyNest.Data;
using BeautyNest.Models.Domain;
using BeautyNest.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BeautyNest.Repositories.Implementation
{
    public class GradRepository : IGradRepository
    {
        private readonly ApplicationDbContext dbContext;

        public GradRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Grad>> GetAllAsync()
        {
            return await dbContext.Gradovi.ToListAsync();
        }
    }
}
