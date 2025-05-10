using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HostelFinder.Data;
using HostelFinder.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HostelFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminLoginDto loginDto)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == loginDto.Username);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, admin.PasswordHash))
            {
                return Unauthorized("Invalid credentials.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, admin.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            Response.Cookies.Append("token", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to true in production
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddMinutes(30)
            });

            return Ok(new { message = "Login successful" });
        }

        [Authorize]
        [HttpGet("validate")]
        public IActionResult Validate()
        {
            return Ok(new { valid = true });
        }


        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("token", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true, // true if you're using HTTPS
                SameSite = SameSiteMode.None
            });

            return Ok(new { message = "Logged out successfully" });
        }

    }
}

