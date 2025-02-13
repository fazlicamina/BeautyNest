using BeautyNest.Data;
using BeautyNest.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeautyNest.Services
{
    public class RecenzijaService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly AuthDbContext authDbContext;

        public RecenzijaService(ApplicationDbContext applicationDbContext, AuthDbContext authDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.authDbContext = authDbContext;
        }

        public async Task<Recenzije> DodajRecenzijuAsync(string klijentId, int rezervacijaId, int ocjena, string tekst, List<IFormFile>? slike)
        {
            var rezervacija = await applicationDbContext.Rezervacije
                .Include(r => r.UslugeRezervacija)
                .ThenInclude(ur => ur.Usluga)
                .FirstOrDefaultAsync(r => r.Id == rezervacijaId && r.KlijentId == klijentId);

            if (rezervacija == null)
                throw new Exception("Nevažeća rezervacija ili pristup nije dozvoljen.");

 
            // Čuvanje slika na disku i pravljenje putanja
            var imagePaths = new List<string>();
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Proveravamo da li ima slika pre nego što iteriramo kroz listu
            if (slike != null && slike.Count > 0)
            {
                foreach (var file in slike)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(uploadFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    imagePaths.Add($"/uploads/{fileName}"); 
                }
            }


            var recenzija = new Recenzije
            {
                KlijentId = klijentId,
                SalonId = rezervacija.SalonId,
                RezervacijaId = rezervacija.Id,
                Ocjena = ocjena,
                Tekst = tekst,
                Slike = imagePaths.Any() ? string.Join(",", imagePaths) : null // Ako nema slika, postavi na null
            };

            applicationDbContext.Recenzije.Add(recenzija);
            rezervacija.HasRecenzija = true;
            await applicationDbContext.SaveChangesAsync();

            // Ažuriranje prosečne ocene salona
            var sveRecenzije = await applicationDbContext.Recenzije
                .Where(r => r.SalonId == rezervacija.SalonId)
                .ToListAsync();

            var prosjecnaOcjena = sveRecenzije.Average(r => r.Ocjena);
            var salon = await applicationDbContext.Saloni.FindAsync(rezervacija.SalonId);
            if (salon != null)
            {
                salon.ProsjecnaOcjena = prosjecnaOcjena;
                await applicationDbContext.SaveChangesAsync();
            }

            return recenzija;
        }


        public async Task<List<Recenzije>> GetRecenzijeZaSalonAsync(int salonId)
        {
            return await applicationDbContext.Recenzije
                .Where(r => r.SalonId == salonId)
                .Include(r => r.Rezervacija)
                .ThenInclude(rez => rez.UslugeRezervacija)
                .ThenInclude(ur => ur.Usluga)
                .ToListAsync();
        }

        public async Task<bool> ObrisiRecenzijuAsync(int recenzijaId)
        {
            var recenzija = await applicationDbContext.Recenzije
                .FirstOrDefaultAsync(r => r.Id == recenzijaId);

            if (recenzija == null)
                return false;

            var rezervacija = await applicationDbContext.Rezervacije
                .FirstOrDefaultAsync(r => r.Id == recenzija.RezervacijaId);

            if (rezervacija != null)
            {
                rezervacija.HasRecenzija = false;
            }

            applicationDbContext.Recenzije.Remove(recenzija);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }



    }

}
