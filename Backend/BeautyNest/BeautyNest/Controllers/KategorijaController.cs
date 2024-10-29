using BeautyNest.Models.DTO;
using BeautyNest.Repositories.Implementation;
using BeautyNest.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautyNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategorijaController : ControllerBase
    {
        private readonly IKategorijaRepository kategorijaRepository;

        public KategorijaController(IKategorijaRepository kategorijaRepository)
        {
            this.kategorijaRepository = kategorijaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKategorije()
        {
            var kategorije = await kategorijaRepository.GetAllAsync();

            var response = kategorije.Select(kategorija => new KategorijaDto
            {
                Id = kategorija.Id,
                Naziv = kategorija.Naziv,
                Slika=kategorija.Slika
            }).ToList();

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetKategorijaById( [FromRoute] int id)
        {
            var kategorija = await kategorijaRepository.GetById(id);
            if (kategorija == null) { return NotFound(); }
            var response = new KategorijaDto {
            Id=kategorija.Id,
            Naziv=kategorija.Naziv,
            Slika=kategorija.Slika};
            return Ok(response);    
        }

    }
}
