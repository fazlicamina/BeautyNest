using BeautyNest.Data;
using BeautyNest.Migrations;
using BeautyNest.Models.Domain;
using BeautyNest.Models.DTO;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BeautyNest.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly AuthDbContext authDbContext;

        public ReservationService(ApplicationDbContext applicationDbContext, AuthDbContext authDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.authDbContext = authDbContext;
        }


        public List<SlobodniTerminDto> GetAvailableTimeSlots(int salonId, DateTime date, List<int> uslugaIds, int brojUposlenika)
        {
            var usluge = applicationDbContext.Usluge.Where(u => uslugaIds.Contains(u.Id)).ToList();
            var salon = applicationDbContext.Saloni.FirstOrDefault(s => s.Id == salonId);
            if (salon == null) throw new Exception("Salon nije pronađen.");

            var radnoVrijemeOd = date.Date.Add(salon.RadnoVrijemeOd);
            var radnoVrijemeDo = date.Date.Add(salon.RadnoVrijemeDo);

            var zauzetiTermini = applicationDbContext.Rezervacije
                .Where(r => r.SalonId == salonId && r.DatumRezervacije.Date == date.Date)
                .Select(r => new { r.VrijemePocetka, r.VrijemeZavrsetka })
                .ToList();

            var ukupnoTrajanje = TimeSpan.FromMinutes(usluge.Sum(u => u.Trajanje.TotalMinutes));
            var slotDuration = TimeSpan.FromMinutes(30);

            var slobodniTermini = new List<SlobodniTerminDto>();

            for (var vrijeme = radnoVrijemeOd; vrijeme.Add(ukupnoTrajanje) <= radnoVrijemeDo; vrijeme = vrijeme.Add(slotDuration))
            {
                var krajTermina = vrijeme.Add(ukupnoTrajanje);
                var preklapanjePoSlotu = new Dictionary<DateTime, int>();

                var validanTermin = true;

                for (var trenutak = vrijeme; trenutak < krajTermina; trenutak = trenutak.Add(slotDuration))
                {
                    // prebrojava se zauzetost za cijeli opseg termina
                    var preklapanje = zauzetiTermini.Count(z =>
                        trenutak >= date.Date.Add(z.VrijemePocetka) && trenutak < date.Date.Add(z.VrijemeZavrsetka));

                    if (preklapanje >= brojUposlenika)
                    {
                        validanTermin = false;
                        break;
                    }
                }


                if (validanTermin)
                {
                    slobodniTermini.Add(new SlobodniTerminDto
                    {
                        Start = vrijeme.TimeOfDay,
                        End = krajTermina.TimeOfDay
                    });
                }
            }

            Console.WriteLine("Primljeni datum: " + date.ToString());
            Console.WriteLine("Primljeni datum u UTC: " + date.ToUniversalTime().ToString());
            Console.WriteLine("Current TimeZone: " + TimeZoneInfo.Local.Id);


            return slobodniTermini;
        }



        public Rezervacija CreateReservation(int salonId, DateTime date, TimeSpan start, List<int> uslugaIds, string userId)
        {
            var zauzetiTermini = applicationDbContext.Rezervacije
                .Where(r => r.SalonId == salonId && r.DatumRezervacije.Date == date.Date)
                .ToList();

            var usluge = applicationDbContext.Usluge
                .Where(u => uslugaIds.Contains(u.Id))
                .ToList();

            var ukupnoTrajanje = TimeSpan.FromMinutes(usluge.Sum(u => u.Trajanje.TotalMinutes));
            var slotDuration = TimeSpan.FromMinutes(30);

            var krajTermina = start + ukupnoTrajanje;

            var brojUposlenika = authDbContext.Users.Count(u => u.SalonId == salonId);

            foreach (var termin in zauzetiTermini)
            {
                // provjera po svakom petominutnom slotu unutar termina
                for (var trenutak = start; trenutak < krajTermina; trenutak = trenutak.Add(slotDuration))
                {
                    var overlapCount = zauzetiTermini.Count(t =>
                        trenutak >= t.VrijemePocetka && trenutak < t.VrijemeZavrsetka);

                    if (overlapCount >= brojUposlenika)
                    {
                        throw new Exception("Nema dostupnih uposlenika za ovaj termin.");
                    }
                }
            }

            var rezervacija = new Rezervacija
            {
                SalonId = salonId,
                DatumRezervacije = date,
                VrijemePocetka = start,
                VrijemeZavrsetka = krajTermina,
                Status = false,
                KlijentId = userId,
                HasRecenzija = false,
                UslugeRezervacija = usluge.Select(u => new UslugaRezervacija
                {
                    UslugaId = u.Id
                }).ToList()
            };

            applicationDbContext.Rezervacije.Add(rezervacija);
            applicationDbContext.SaveChanges();

            return rezervacija;
        }



        public void UpdateReservationStatus(int rezervacijaId, bool status, string poruka) 
        {

            var rezervacija = applicationDbContext.Rezervacije.FirstOrDefault(r => r.Id == rezervacijaId);
            if (rezervacija != null)
            {
                rezervacija.Status = status;
                rezervacija.Poruka = poruka; 
                applicationDbContext.SaveChanges();
            }
        }

        public int GetBrojUposlenika(int salonId)
        {
           
            var brojUposlenika = authDbContext.Users.Count(u => u.SalonId == salonId);
            return brojUposlenika;
        }

        public Rezervacija GetRezervacijaById(int rezervacijaId)
        {
            return applicationDbContext.Rezervacije.FirstOrDefault(r => r.Id == rezervacijaId);
        }

        public async Task<List<RezervacijaDto>> GetRezervacijeByUserIdAsync(string userId)
        {
            var rezervacije = await applicationDbContext.Rezervacije
                .Where(r => r.KlijentId == userId)
                .Include(r => r.Salon)
                .Include(r => r.UslugeRezervacija)
                    .ThenInclude(ur => ur.Usluga)
                .ToListAsync();

            return rezervacije.Select(r => new RezervacijaDto
            {
                Id = r.Id,
                KlijentId = r.KlijentId,
                SalonId = r.SalonId,
                SalonNaziv = r.Salon?.Naziv ?? "Nepoznato",
                SalonAdresa = r.Salon?.Adresa ?? "Nepoznata adresa",
                DatumRezervacije = r.DatumRezervacije,
                VrijemePocetka = r.VrijemePocetka,
                VrijemeZavrsetka = r.VrijemeZavrsetka,
                Status = r.Status,
                Poruka = r.Poruka,
                Usluge = r.UslugeRezervacija.Select(ur => ur.Usluga.Naziv).ToList(),
                Trajanje = r.UslugeRezervacija.Sum(ur => ur.Usluga.Trajanje.Minutes),
                HasRecenzija = r.HasRecenzija
            }).ToList();
        }

        public async Task CancelRezervacijaAsync(int rezervacijaId)
        {
            var rezervacija = await applicationDbContext.Rezervacije.FindAsync(rezervacijaId);
            if (rezervacija != null)
            {
                applicationDbContext.Rezervacije.Remove(rezervacija);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        public async Task<bool> OtkaziRezervacijuAsync(int rezervacijaId, string userId)
        {

            var rezervacija = await applicationDbContext.Rezervacije
                .Include(r => r.UslugeRezervacija)
                .FirstOrDefaultAsync(r => r.Id == rezervacijaId && r.KlijentId == userId);

            if (rezervacija == null) return false;

            applicationDbContext.UslugeRezervacije.RemoveRange(rezervacija.UslugeRezervacija);

            applicationDbContext.Rezervacije.Remove(rezervacija);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }




    }
}
