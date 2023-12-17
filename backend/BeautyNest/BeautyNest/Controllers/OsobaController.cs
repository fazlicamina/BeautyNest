using BeautyNest.Klase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautyNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsobaController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Osoba>>> GetOsobe()
        {
            return new List<Osoba>()
            {
                new Osoba() {
                   Ime="ime",
                   Prezime="prezime"
                }
            };
        }
    }
}
