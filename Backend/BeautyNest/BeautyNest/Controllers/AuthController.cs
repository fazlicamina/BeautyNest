using BeautyNest.Models.Domain;
using BeautyNest.Models.DTO;
using BeautyNest.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeautyNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<User> userManager,
            ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var identityUser=await userManager.FindByNameAsync(request.Username);

            if(identityUser is not null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);

                if (checkPasswordResult)
                {
                    var roles= await userManager.GetRolesAsync(identityUser);

                   var jwtToken= tokenRepository.CreateJwtToken(identityUser, roles.ToList());
                    var response = new LoginResponseDto()
                    {
                        Username=request.Username,
                        Roles=roles.ToList(),
                        Token=jwtToken,
                        FirstName = identityUser.FirstName,
                        LastName = identityUser.LastName,
    
                    };
                    return Ok(response);
                }
            }

            ModelState.AddModelError("", "Email or Password incorrect");
            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var user = new User
            {
                UserName = request.UserName?.Trim(),
                Email = request.Email?.Trim(),
                FirstName = request.FirstName?.Trim(),
                LastName = request.LastName?.Trim()
            };

            var identityResult= await userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(user, request.Role);

                if (identityResult.Succeeded )
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }

        [HttpGet]
        [Route("mojprofil")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
                return Unauthorized(new { message = "Niste autentificirani.", claims });
            }

            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound(new { message = "Korisnik nije pronađen." });
            }

            var roles = await userManager.GetRolesAsync(user);

            var userDto = new UserProfileDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = user.ProfilePicture,
                Roles = roles.ToList()
            };

            return Ok(userDto);
        }


    }
}
