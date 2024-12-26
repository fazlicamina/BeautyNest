using BeautyNest.Data;
using BeautyNest.Models.Domain;
using BeautyNest.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BeautyNest.Repositories.Implementation
{
    public class RezervacijaRepository:IRezervacijaRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RezervacijaRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Rezervacija>> DohvatiRezervacijeZaSalonAsync(int salonId, DateTime datum)
        {
            return await dbContext.Rezervacije
                .Where(r => r.SalonId == salonId && r.DatumRezervacije.Date == datum.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rezervacija>> DohvatiSveRezervacijeKorisnikaAsync(string userId)
        {
            return await dbContext.Rezervacije
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> ProvjeriDostupnostAsync(int salonId, DateTime datum, TimeSpan vrijemePocetka, TimeSpan vrijemeZavrsetka)
        {
            var rezervacije = await DohvatiRezervacijeZaSalonAsync(salonId, datum);

            return !rezervacije.Any(r =>
                (vrijemePocetka < r.VrijemeZavrsetka && vrijemeZavrsetka > r.VrijemePocetka));
        }

        public async Task<Rezervacija> DodajRezervacijuAsync(Rezervacija rezervacija)
        {
            await dbContext.Rezervacije.AddAsync(rezervacija);
            await dbContext.SaveChangesAsync();
            return rezervacija;
        }

        public async Task<bool> ObrisiRezervacijuAsync(int rezervacijaId)
        {
            var rezervacija = await dbContext.Rezervacije.FindAsync(rezervacijaId);
            if (rezervacija == null) return false;

            dbContext.Rezervacije.Remove(rezervacija);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
