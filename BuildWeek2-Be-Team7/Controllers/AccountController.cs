using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuildWeek2_Be_Team7.DTOs.Account;
using BuildWeek2_Be_Team7.Models.Auth;
using BuildWeek2_Be_Team7.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BuildWeek2_Be_Team7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Jwt _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(Jwt jwtSettings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {

            if (registerRequest == null)
            {
                return BadRequest(new
                {
                    message = "Ops qualcosa e' andato storto"
                });
            }
            var newUser = new ApplicationUser()
            {
                Email = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                UserName = registerRequest.Email,
                CodiceFiscale = registerRequest.CodiceFiscale,
                Birthdate = registerRequest.BirthDate,
               
            };
            var result = await _userManager.CreateAsync(newUser, registerRequest.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Ops qualcosa e' andato storto"
                });
            }
            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user == null)
            {
                return BadRequest(new
                {
                    message = "Ops qualcosa e' andato storto"
                });
            }

            await _userManager.AddToRoleAsync(user, "Utente");

            return Ok(new
            {
                message = "Utente Registrato Correttamente"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return BadRequest(new
                {
                    message = "Ops qualcosa e' andato storto"
                });
            }
            await _signInManager.PasswordSignInAsync(user, loginRequest.Password, true, false);
            var roles = await _signInManager.UserManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes);

            var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims, expires: expiry, signingCredentials: creds);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new TokenResponse()
            {
                Token = tokenString,
                Expires = expiry,
            });
        }



    }
}
