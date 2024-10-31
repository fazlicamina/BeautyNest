using BeautyNest.Models.Domain;
using BeautyNest.Models.DTO;
using BeautyNest.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautyNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategorijaUslugeController : ControllerBase
    {
        private readonly IKategorijaUslugeRepository kategorijaUslugeRepository;

        public KategorijaUslugeController(IKategorijaUslugeRepository kategorijaUslugeRepository)
        {
            this.kategorijaUslugeRepository = kategorijaUslugeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateKategorijaUsluge (CreateKategorijaUslugeRequestDto request)
        {
            var kategorijaUsluge = new KategorijaUsluge
            {
                Naziv=request.Naziv
            };

            await kategorijaUslugeRepository.CreateAsync (kategorijaUsluge);

            var response = new KategorijaUslugeDto
            {
                Id=kategorijaUsluge.Id,
                Naziv=kategorijaUsluge.Naziv

            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKategorijeUsluga()
        {
            var kategorijaUsluge = await kategorijaUslugeRepository.GetAllAsync();

            var response = kategorijaUsluge.Select(k => new KategorijaUslugeDto
            {
                Id = k.Id,
                Naziv = k.Naziv,
                SalonId=k.SalonId
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetKategorijaUslugeById(int id)
        {
            var kategorijaUsluge = await kategorijaUslugeRepository.GetByIdAsync(id);
            if (kategorijaUsluge == null)
            {
                return NotFound();
            }

            var response = new KategorijaUslugeDto
            {
                Id = kategorijaUsluge.Id,
                Naziv = kategorijaUsluge.Naziv,
                Usluge = kategorijaUsluge.Usluge.Select(u => new UslugaDto
                {
                    Id = u.Id,
                    Naziv = u.Naziv,
                    Cijena = u.Cijena,
                    Trajanje = u.Trajanje,
                    KategorijaUslugeId = u.KategorijaUslugeId,
                    KategorijaNaziv = u.KategorijaUsluge?.Naziv
                }).ToList()
            };

            return Ok(response);
        }
    }
}
