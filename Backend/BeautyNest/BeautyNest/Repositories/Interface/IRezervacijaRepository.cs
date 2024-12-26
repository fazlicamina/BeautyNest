using BeautyNest.Models.Domain;

namespace BeautyNest.Repositories.Interface
{
    public interface IRezervacijaRepository
    {
        Task<IEnumerable<Rezervacija>> DohvatiRezervacijeZaSalonAsync(int salonId, DateTime datum);
        Task<IEnumerable<Rezervacija>> DohvatiSveRezervacijeKorisnikaAsync(string userId);
        Task<bool> ProvjeriDostupnostAsync(int salonId, DateTime datum, TimeSpan vrijemePocetka, TimeSpan vrijemeZavrsetka);
        Task<Rezervacija> DodajRezervacijuAsync(Rezervacija rezervacija);
        Task<bool> ObrisiRezervacijuAsync(int rezervacijaId);
    }
}
