using BeautyNest.Data;
using BeautyNest.Models.Domain;
using BeautyNest.Models.DTO;
using Microsoft.EntityFrameworkCore;

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
                Status = false
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


    }
}
