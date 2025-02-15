using BeautyNest.Models.Domain;
using BeautyNest.Models.DTO;
using BeautyNest.Repositories.Implementation;
using BeautyNest.Repositories.Interface;
using BeautyNest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Web;

namespace BeautyNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly EmailService emailService;


        public AuthController(UserManager<User> userManager,
            ITokenRepository tokenRepository, EmailService emailService)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.emailService = emailService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var identityUser = await userManager.FindByNameAsync(request.Username);

            if (identityUser != null)
            {

                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);

                if (checkPasswordResult)
                {
                    if (!await userManager.IsEmailConfirmedAsync(identityUser))
                    {
                        ModelState.AddModelError("", "Molimo vas da aktivirate svoj email prije nego što se prijavite!"!);
                        return ValidationProblem(ModelState);
                    }

                    var roles = await userManager.GetRolesAsync(identityUser);
                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());

                    var response = new LoginResponseDto()
                    {
                        Username = request.Username,
                        Roles = roles.ToList(),
                        Token = jwtToken,
                        FirstName = identityUser.FirstName,
                        LastName = identityUser.LastName,
                        Id = identityUser.Id
                    };

                    return Ok(response);
                }
            }

            ModelState.AddModelError("", "Korisničko ime ili lozinka nisu tačni.");
            return ValidationProblem(ModelState);
        }



        [HttpPost]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return NotFound("Korisnik nije pronađen.");

            if (await userManager.IsEmailConfirmedAsync(user))
                return BadRequest("Email je već potvrđen.");

            string decodedToken = HttpUtility.UrlDecode(request.Token);
            var result = await userManager.ConfirmEmailAsync(user, decodedToken);

          

            if (result.Succeeded)
                return Ok("Email je uspješno potvrđen!");

            return BadRequest("Token nije validan ili je već iskorišten.");
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

            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(user, request.Role);
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = HttpUtility.UrlEncode(token);

                var confirmationLink = $"http://localhost:4200/activate?email={user.Email}&token={encodedToken}";
                var emailBody = $@"
            <h2>Dobrodošli u BeautyNest!</h2>
            <p>Hvala vam što ste se registrovali. Kako biste nastavili s korištenjem platforme, potrebno je potvrditi vaš nalog.</p>
            <p>Kliknite na sljedeći link da aktivirate svoj nalog:</p>
            <p><a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>Aktiviraj nalog</a></p>
            <p>Ukoliko niste vi izvršili registraciju, slobodno zanemarite ovaj email.</p>
            <p>Srdačan pozdrav,<br>Beauty Nest tim</p>";

                await emailService.SendEmailAsync(user.Email, "Dobrodošli u BeautyNest – Potvrdite svoj nalog", emailBody);

                return Ok("Registracija uspješna! Provjerite email za aktivaciju naloga.");
            }

            return BadRequest(identityResult.Errors);
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
        
                if (!profilePicture.ContentType.StartsWith("image"))
                {
                    return BadRequest(new { message = "Morate uploadovati sliku." });
                }


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
