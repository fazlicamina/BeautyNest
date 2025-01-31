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
        public async Task<IActionResult> DodajRecenziju([FromBody] RecenzijaRequestDto request)
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
            return Ok(recenzije);
        }
    }
}
