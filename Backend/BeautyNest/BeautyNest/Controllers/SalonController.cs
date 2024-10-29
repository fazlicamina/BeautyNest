using BeautyNest.Data;
using BeautyNest.Models.Domain;
using BeautyNest.Models.DTO;
using BeautyNest.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautyNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        private readonly ISalonRepository salonRepository;
        private readonly IKategorijaRepository kategorijaRepository;

        public SalonController(ISalonRepository salonRepository, IKategorijaRepository kategorijaRepository)
        {
            this.salonRepository = salonRepository;
            this.kategorijaRepository = kategorijaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalon (CreateSalonRequestDto request)
        {

            var salon = new Salon
            {
                Naziv = request.Naziv,
                Adresa = request.Adresa,
                KontaktTelefon = request.KontaktTelefon,
                Email = request.Email,
                Opis = request.Opis,
                RadnoVrijemeOd = request.RadnoVrijemeOd,
                RadnoVrijemeDo = request.RadnoVrijemeDo,
                SubotaRadna = request.SubotaRadna,
                NaslovnaFotografija = request.NaslovnaFotografija,
                Kategorije = new List<Kategorija>()
            };

            foreach (var kategorijaId in request.Kategorije)
            {
                var existingKategorija = await kategorijaRepository.GetById(kategorijaId);
                if (existingKategorija is not null)
                {
                    salon.Kategorije.Add(existingKategorija);   
                }
            }

            await salonRepository.CreateAsync(salon);

            var response = new SalonDto
            {
                Id = salon.Id,
                Naziv = salon.Naziv,
                Adresa = salon.Adresa,
                KontaktTelefon = salon.KontaktTelefon,
                Email = salon.Email,
                ProsjecnaOcjena = salon.ProsjecnaOcjena,
                Opis = salon.Opis,
                RadnoVrijemeOd = salon.RadnoVrijemeOd,
                RadnoVrijemeDo = salon.RadnoVrijemeDo,
                SubotaRadna = salon.SubotaRadna,
                NaslovnaFotografija = salon.NaslovnaFotografija,
                Kategorije = salon.Kategorije.Select(k => new KategorijaDto
                {
                    Id = k.Id,
                    Naziv = k.Naziv,
                    Slika = k.Slika
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSaloni()
        {
            var salons = await salonRepository.GetAllAsync();

            var response = new List<SalonDto>();

            foreach (var salon in salons)
            {
                response.Add(new SalonDto
                {
                    Id = salon.Id,
                    Naziv = salon.Naziv,
                    Adresa = salon.Adresa,
                    KontaktTelefon = salon.KontaktTelefon,
                    Email = salon.Email,
                    ProsjecnaOcjena = salon.ProsjecnaOcjena,
                    Opis = salon.Opis,
                    RadnoVrijemeOd = salon.RadnoVrijemeOd,
                    RadnoVrijemeDo = salon.RadnoVrijemeDo,
                    SubotaRadna = salon.SubotaRadna,
                    NaslovnaFotografija = salon.NaslovnaFotografija,
                    Kategorije = salon.Kategorije.Select(k => new KategorijaDto
                    {
                        Id = k.Id,
                        Naziv = k.Naziv,
                        Slika = k.Slika
                    }).ToList()
                });
            }

            return Ok(response);
        }



        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteSalon([FromRoute] int id)
        {
            var salon=await salonRepository.DeleteAsync(id);
            if (salon == null) { return NotFound(); }
            var response = new SalonDto
            {
                Id = salon.Id,
                Naziv = salon.Naziv,
                Adresa = salon.Adresa,
                KontaktTelefon = salon.KontaktTelefon,
                Email = salon.Email,
                ProsjecnaOcjena = salon.ProsjecnaOcjena,
                Opis = salon.Opis,
                RadnoVrijemeOd = salon.RadnoVrijemeOd,
                RadnoVrijemeDo = salon.RadnoVrijemeDo,
                SubotaRadna = salon.SubotaRadna,
                NaslovnaFotografija = salon.NaslovnaFotografija
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetSalonById([FromRoute] int id)
        {
            var salon = await salonRepository.GetByIdAsync(id);
            if (salon == null) { return NotFound(); }
            var response = new SalonDto
            {
                Id = salon.Id,
                Naziv = salon.Naziv,
                Adresa = salon.Adresa,
                KontaktTelefon = salon.KontaktTelefon,
                Email = salon.Email,
                ProsjecnaOcjena = salon.ProsjecnaOcjena,
                Opis = salon.Opis,
                RadnoVrijemeOd = salon.RadnoVrijemeOd,
                RadnoVrijemeDo = salon.RadnoVrijemeDo,
                SubotaRadna = salon.SubotaRadna,
                NaslovnaFotografija = salon.NaslovnaFotografija,
                Kategorije = salon.Kategorije.Select(k => new KategorijaDto
                {
                    Id = k.Id,
                    Naziv = k.Naziv,
                    Slika = k.Slika
                }).ToList()
            };
            return Ok(response);
        }

    }
}
