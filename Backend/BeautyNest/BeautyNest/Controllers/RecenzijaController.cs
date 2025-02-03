using BeautyNest.Models.DTO;
using BeautyNest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautyNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecenzijaController : ControllerBase
    {
        private readonly RecenzijaService recenzijaService;

        public RecenzijaController(RecenzijaService recenzijaService)
        {
            this.recenzijaService = recenzijaService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DodajRecenziju([FromForm] RecenzijaRequestDto request)
        {
            var klijentId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(klijentId))
                return Unauthorized();

            try
            {
                var recenzija = await recenzijaService.DodajRecenzijuAsync(
                    klijentId, request.RezervacijaId, request.Ocjena, request.Tekst, request.Slike
                );

                return Ok(recenzija);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{salonId}")]
        public async Task<IActionResult> GetRecenzijeZaSalon(int salonId)
        {
            var recenzije = await recenzijaService.GetRecenzijeZaSalonAsync(salonId);

            var recenzijeDto = recenzije.Select(r => new RecenzijaResponseDto
            {
                Id = r.Id,
                KlijentId = r.KlijentId,
                SalonId = r.SalonId,
                RezervacijaId = r.RezervacijaId,
                Ocjena = r.Ocjena,
                Tekst = r.Tekst,
                DatumRecenzije = r.DatumRecenzije,
                Usluge = r.Rezervacija?.UslugeRezervacija?.Select(ur => new UslugaDto
                {
                    Id = ur.Usluga.Id,
                    Naziv = ur.Usluga.Naziv,
                    Cijena = ur.Usluga.Cijena,
                    Trajanje = ur.Usluga.Trajanje,
                    KategorijaUslugeId = ur.Usluga.KategorijaUslugeId,
                    KategorijaNaziv = ur.Usluga.KategorijaUsluge?.Naziv ?? string.Empty
                }).ToList() ?? new List<UslugaDto>(),
                Slike = r.Slike?.Split(',').ToList() ?? new List<string>() // Razdvajamo putanje u listu
            }).ToList();

            return Ok(recenzijeDto);
        }

    }
}
