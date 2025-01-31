using BeautyNest.Models.Domain;
using BeautyNest.Models.DTO;
using BeautyNest.Repositories.Implementation;
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

        //login

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
                        Id = identityUser.Id

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


        [HttpPut]
        [Route("editprofile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileDto request, [FromForm] IFormFile? profilePicture)
        {
            var userName = User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized(new { message = "Niste autentificirani." });
            }

            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound(new { message = "Korisnik nije pronađen." });
            }

            user.FirstName = request.FirstName?.Trim() ?? user.FirstName;
            user.LastName = request.LastName?.Trim() ?? user.LastName;
            user.Email = request.Email?.Trim() ?? user.Email;

            if (profilePicture != null)
            {
                // Djelomična validacija za tip slike (ako je potrebno)
                if (!profilePicture.ContentType.StartsWith("image"))
                {
                    return BadRequest(new { message = "Morate uploadovati sliku." });
                }

                // Spremanje slike kao byte[] u bazu podataka
                using (var memoryStream = new MemoryStream())
                {
                    await profilePicture.CopyToAsync(memoryStream);
                    user.ProfilePicture = memoryStream.ToArray();
                }
            }

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return ValidationProblem(ModelState);
            }

            return Ok(new { message = "Profil je uspješno ažuriran." });
        }


        [HttpGet]
        [Route("user/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound(new { message = "Korisnik nije pronađen." });
            }

            var userDto = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null
            };

            return Ok(userDto);
        }



    }
}
