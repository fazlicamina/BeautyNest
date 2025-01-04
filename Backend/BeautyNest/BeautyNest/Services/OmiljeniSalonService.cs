using BeautyNest.Data;
using BeautyNest.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeautyNest.Services
{
    public class OmiljeniSalonService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly AuthDbContext authDbContext;

        public OmiljeniSalonService(ApplicationDbContext applicationDbContext, AuthDbContext authDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.authDbContext = authDbContext;
        }

        public async Task<bool> AddOmiljeniSalonAsync(string userId, int salonId)
        {
            var existing = await applicationDbContext.OmiljeniSaloni
                .FirstOrDefaultAsync(x => x.UserId == userId && x.SalonId == salonId);
            if (existing != null) return false;

            var omiljeniSalon = new OmiljeniSalon { UserId = userId, SalonId = salonId };
            applicationDbContext.OmiljeniSaloni.Add(omiljeniSalon);
            await applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Salon>> GetOmiljeniSaloniAsync(string userId)
        {
            var salonIds = await applicationDbContext.OmiljeniSaloni
                .Where(x => x.UserId == userId)
                .Select(x => x.SalonId)
                .ToListAsync();

            return await applicationDbContext.Saloni
                .Where(salon => salonIds.Contains(salon.Id))
                .ToListAsync();
        }

        public async Task<bool> ToggleOmiljeniSalonAsync(string userId, int salonId)
        {
            var omiljeniSalon = await applicationDbContext.OmiljeniSaloni
                .FirstOrDefaultAsync(x => x.UserId == userId && x.SalonId == salonId);

            if (omiljeniSalon != null)
            {
                // Ako već postoji, ukloni ga iz omiljenih
                applicationDbContext.OmiljeniSaloni.Remove(omiljeniSalon);
                await applicationDbContext.SaveChangesAsync();
                return false; // Vraća false ako je uklonjen
            }

            // Ako ne postoji, dodaj ga u omiljene
            omiljeniSalon = new OmiljeniSalon
            {
                UserId = userId,
                SalonId = salonId
            };

            await applicationDbContext.OmiljeniSaloni.AddAsync(omiljeniSalon);
            await applicationDbContext.SaveChangesAsync();
            return true; // Vraća true ako je dodan
        }



    }
}
