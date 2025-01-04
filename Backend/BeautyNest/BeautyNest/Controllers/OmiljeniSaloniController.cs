using BeautyNest.Data;
using BeautyNest.Models.Domain;
using BeautyNest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautyNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OmiljeniSaloniController : ControllerBase
    {
        private readonly OmiljeniSalonService omiljeniSalonService;

        public OmiljeniSaloniController(OmiljeniSalonService omiljeniSalonService)
        {
            this.omiljeniSalonService = omiljeniSalonService;
        }

        [HttpPost]
        [Route("toggle")]
        [Authorize]
        public async Task<IActionResult> ToggleOmiljeniSalon(int salonId)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var isAdded = await omiljeniSalonService.ToggleOmiljeniSalonAsync(userId, salonId);

            if (isAdded)
            {
                return Ok(new { message = "Salon je uspješno dodan u omiljene." });
            }

            return Ok(new { message = "Salon je uklonjen iz omiljenih." });
        }


        [HttpGet]
        [Route("list")]
        [Authorize]
        public async Task<IActionResult> GetOmiljeniSaloni()
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var omiljeniSaloni = await omiljeniSalonService.GetOmiljeniSaloniAsync(userId);
            return Ok(omiljeniSaloni);
        }
    }
}
