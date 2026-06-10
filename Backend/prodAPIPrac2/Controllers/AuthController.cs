using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using prodAPIPrac2.data;
using prodAPIPrac2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace prodAPIPrac2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly appdbcontext _context;


        public AuthController(IConfiguration configuration,
            appdbcontext context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var existingUser = await _context.users
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (existingUser != null)
            {
                return BadRequest("Email already exists");
            }

            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Role = "User"
            };

            user.PasswordHash =
                passwordHasher.HashPassword(user, model.Password);

            _context.users.Add(user);

            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully" });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(loginmodel login)
        {
            var user = await _context.users
                .FirstOrDefaultAsync(x => x.Email == login.Email);
           
            if (user == null)
            {
                return Unauthorized("Invalid Email or Password");
            }

            var passwordHasher = new PasswordHasher<User>();

            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                login.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid Email or Password");
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                userName = user.Name,
                email = user.Email,
                role = user.Role
            });
        }
    }
}
    

