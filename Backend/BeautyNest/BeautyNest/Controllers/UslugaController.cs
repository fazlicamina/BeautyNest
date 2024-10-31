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
    public class UslugaController : ControllerBase
    {
        private readonly IUslugaRepository uslugaRepository;

        public UslugaController(IUslugaRepository uslugaRepository)
        {
            this.uslugaRepository = uslugaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsluga(CreateUslugaRequestDto request)
        {
            var usluga = new Usluga
            {
                Naziv = request.Naziv,
                Cijena=request.Cijena,
                Trajanje=request.Trajanje,
                KategorijaUslugeId = request.KategorijaUslugeId
            };

            await uslugaRepository.CreateAsync(usluga);

            var response = new UslugaDto
            {
                Id = usluga.Id,
                Naziv = usluga.Naziv,
                Cijena=usluga.Cijena,
                Trajanje=usluga.Trajanje,
                KategorijaUslugeId = usluga.KategorijaUslugeId,
                KategorijaNaziv = usluga.KategorijaUsluge?.Naziv
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsluge()
        {
            var usluge = await uslugaRepository.GetAllAsync();

            var response = usluge.Select(k => new UslugaDto
            {
                Id = k.Id,
                Naziv = k.Naziv,
                Cijena=k.Cijena,
                Trajanje=k.Trajanje,
                KategorijaUslugeId = k.KategorijaUslugeId,
                KategorijaNaziv = k.KategorijaUsluge?.Naziv

            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUslugaById(int id)
        {
            var usluga = await uslugaRepository.GetByIdAsync(id);
            if (usluga == null)
            {
                return NotFound();
            }

            var response = new UslugaDto
            {
                Id = usluga.Id,
                Naziv = usluga.Naziv,
                Cijena=usluga.Cijena,
                Trajanje=usluga.Trajanje,
                KategorijaUslugeId = usluga.KategorijaUslugeId,
                KategorijaNaziv = usluga.KategorijaUsluge?.Naziv
            };

            return Ok(response);
        }
    }
}
