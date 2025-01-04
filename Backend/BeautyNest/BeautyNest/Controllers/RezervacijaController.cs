using BeautyNest.Models.Domain;
using BeautyNest.Models.DTO;
using BeautyNest.Repositories.Implementation;
using BeautyNest.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautyNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervacijaController : ControllerBase
    {
        private readonly IRezervacijaRepository rezervacijaRepository;
        private readonly ISalonRepository salonRepository;
        private readonly IUslugaRepository uslugaRepository;

        public RezervacijaController(IRezervacijaRepository rezervacijaRepository, ISalonRepository salonRepository, IUslugaRepository uslugaRepository)
        {
            this.rezervacijaRepository = rezervacijaRepository;
            this.salonRepository = salonRepository;
            this.uslugaRepository = uslugaRepository;
        }


        [HttpGet]
        [Route("DostupniSlotovi")]
        public async Task<IActionResult> DohvatiDostupneSlotove(
        int salonId,
            DateTime datum)
        {
            var salon = await salonRepository.GetByIdAsync(salonId);
            if (salon == null)
                return NotFound("Salon nije pronađen.");

            var rezervacije = await rezervacijaRepository.DohvatiRezervacijeZaSalonAsync(salonId, datum);

            var slotovi = GenerisiSlotove(
                salon.RadnoVrijemeOd,
                salon.RadnoVrijemeDo,
                rezervacije);

            return Ok(slotovi);
        }



        [HttpPost]
        public async Task<IActionResult> KreirajRezervaciju(CreateRezervacijaRequestDto dto)
        {
            var salon = await salonRepository.GetByIdAsync(dto.SalonId);
            if (salon == null)
                return NotFound("Salon nije pronađen.");

            var usluge = await uslugaRepository.GetByIdsAsync(dto.UslugaIds); // Pretpostavljamo da postoji metoda GetByIdsAsync

            if (usluge.Count != dto.UslugaIds.Count)
                return NotFound("Neke usluge nisu pronađene.");

            // Logika za proveru dostupnosti
            var vrijemeZavrsetka = dto.VrijemePocetka; // Start time of first service, you can modify this logic as needed

            foreach (var usluga in usluge)
            {
                vrijemeZavrsetka = vrijemeZavrsetka + usluga.Trajanje;
            }

            var dostupnost = await rezervacijaRepository.ProvjeriDostupnostAsync(
                dto.SalonId,
                dto.DatumRezervacije,
                dto.VrijemePocetka,
                vrijemeZavrsetka);

            if (!dostupnost)
                return Conflict("Termin nije dostupan.");

            var rezervacija = new Rezervacija
            {
                SalonId = dto.SalonId,
                DatumRezervacije = dto.DatumRezervacije,
                VrijemePocetka = dto.VrijemePocetka,
                VrijemeZavrsetka = vrijemeZavrsetka,
                Status = true
            };

            // Dodavanje usluga u rezervaciju
            foreach (var usluga in usluge)
            {
                rezervacija.UslugeRezervacija.Add(new UslugaRezervacija
                {
                    Usluga = usluga
                });
            }

            var novaRezervacija = await rezervacijaRepository.DodajRezervacijuAsync(rezervacija);

            // Popuni DTO sa uslugama
            var rezervacijaDto = new RezervacijaDto
            {
                Id = novaRezervacija.Id,
                SalonId = novaRezervacija.SalonId,
                DatumRezervacije = novaRezervacija.DatumRezervacije,
                VrijemePocetka = novaRezervacija.VrijemePocetka,
                VrijemeZavrsetka = novaRezervacija.VrijemeZavrsetka,
                Status = novaRezervacija.Status,
                Usluge = usluge.Select(u => new UslugaDto
                {
                    Id = u.Id,
                    Naziv = u.Naziv,
                    Trajanje = u.Trajanje,
                    Cijena=u.Cijena,
                    KategorijaUslugeId=u.KategorijaUslugeId,
                    KategorijaNaziv=u.KategorijaUsluge.Naziv
                }).ToList()  
            };

            return CreatedAtAction(nameof(DohvatiDostupneSlotove), new { salonId = dto.SalonId, datum = dto.DatumRezervacije }, rezervacijaDto);
        }







        [HttpDelete("{id}")]
        public async Task<IActionResult> ObrisiRezervaciju(int id)
        {
            var rezultat = await rezervacijaRepository.ObrisiRezervacijuAsync(id);

            if (!rezultat)
                return NotFound("Rezervacija nije pronađena.");

            return NoContent();
        }

        private IEnumerable<TimeSpan> GenerisiSlotove(
            TimeSpan radnoVrijemeOd,
            TimeSpan radnoVrijemeDo,
            IEnumerable<Rezervacija> rezervacije)
        {
            var slotovi = new List<TimeSpan>();

            for (var vrijeme = radnoVrijemeOd; vrijeme < radnoVrijemeDo; vrijeme += TimeSpan.FromMinutes(30))
            {
                var zauzeto = rezervacije.Any(r =>
                    vrijeme < r.VrijemeZavrsetka &&
                    vrijeme + TimeSpan.FromMinutes(30) > r.VrijemePocetka);

                if (!zauzeto)
                    slotovi.Add(vrijeme);
            }

            return slotovi;
        }

    }
}
