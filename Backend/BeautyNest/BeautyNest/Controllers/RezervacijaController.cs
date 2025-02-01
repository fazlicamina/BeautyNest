using BeautyNest.Models.Domain;
using BeautyNest.Models.DTO;
using BeautyNest.Repositories.Implementation;
using BeautyNest.Repositories.Interface;
using BeautyNest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautyNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervacijaController : ControllerBase
    {
        private readonly ReservationService reservationService;

        public RezervacijaController(ReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet("slobodni-termini")]
        public IActionResult GetSlobodniTermini(int salonId, DateTime datum, [FromQuery] List<int> uslugaIds)
        {
            try
            {
                var brojUposlenika = reservationService.GetBrojUposlenika(salonId);

                var slobodniTermini = reservationService.GetAvailableTimeSlots(salonId, datum, uslugaIds, brojUposlenika);
                return Ok(slobodniTermini);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        //Dodavanje nove rezervacije
        [HttpPost]
        [Authorize]
        public IActionResult CreateRezervacija([FromBody] CreateRezervacijaRequestDto request)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest(new { message = "User claim not found." });
                }

                request.KlijentId = userId;

                var rezervacija = reservationService.CreateReservation(
                    request.SalonId,
                    request.DatumRezervacije,
                    request.VrijemePocetka,
                    request.UslugaIds,
                    request.KlijentId
                );

                var response = new RezervacijaDto
                {
                    Id = rezervacija.Id,
                    SalonId = rezervacija.SalonId,
                    DatumRezervacije = rezervacija.DatumRezervacije,
                    VrijemePocetka = rezervacija.VrijemePocetka,
                    VrijemeZavrsetka = rezervacija.VrijemeZavrsetka,
                    Status = rezervacija.Status,
                    Poruka = rezervacija.Poruka,
                    HasRecenzija = rezervacija.HasRecenzija
                };

                return CreatedAtAction(nameof(GetRezervacijaById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        //Prihvatanje/odbijanje rezervacije
        [HttpPut("{id}/status")]
        [Authorize]
        public IActionResult UpdateRezervacijaStatus(int id, [FromBody] RezervacijaStatusRequestDto request)
        {
            try
            {
                reservationService.UpdateReservationStatus(id, request.Status, request.Poruka);  
                return Ok(new { message = "Status rezervacije ažuriran." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // Dohvatanje pojedinačne rezervacije (za CreatedAtAction)
        [HttpGet("{id}")]
        public IActionResult GetRezervacijaById(int id)
        {
            var rezervacija = reservationService.GetRezervacijaById(id);
            if (rezervacija == null) return NotFound();

            return Ok(rezervacija);
        }

        [HttpGet("moje-rezervacije")]
        [Authorize]
        public async Task<IActionResult> GetMojeRezervacije()
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var rezervacije = await reservationService.GetRezervacijeByUserIdAsync(userId);
            return Ok(rezervacije);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> OtkaziRezervaciju(int id)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var success = await reservationService.OtkaziRezervacijuAsync(id, userId);
            return Ok(new { message = "Rezervacija je uspješno otkazana." });
        }





    }
}
