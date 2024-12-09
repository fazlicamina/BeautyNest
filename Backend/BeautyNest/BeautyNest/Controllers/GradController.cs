using BeautyNest.Models.DTO;
using BeautyNest.Repositories.Implementation;
using BeautyNest.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautyNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradController : ControllerBase
    {
        private readonly IGradRepository gradRepository;

        public GradController(IGradRepository gradRepository)
        {
            this.gradRepository = gradRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGradovi()
        {
            var gradovi = await gradRepository.GetAllAsync();

            var response = gradovi.Select(x => new GradDto
            {
                Id= x.Id,
                Naziv=x.Naziv
            });

            return Ok(response);
           
        }





    }
}
