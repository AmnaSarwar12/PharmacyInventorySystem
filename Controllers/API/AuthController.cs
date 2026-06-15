using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyInventorySystem.Data;
using PharmacyInventorySystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
namespace PharmacyInventorySystem.Controllers.API
{
   [Route("api/[controller]")] 
   [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Username and password are required.");
            }
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return BadRequest("Username already exists.");
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User registered successfully.");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            var sw = Stopwatch.StartNew();
            bool canConnect = _context.Database.CanConnect();

            Console.WriteLine($"Database connect: {sw.ElapsedMilliseconds} ms");
            if(login == null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("username and password are required.");
            }
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username);
            Console.WriteLine($"User lookup: {sw.ElapsedMilliseconds} ms");
            if(user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            //verify password against hash
            bool isValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
            Console.WriteLine($"Password verify: {sw.ElapsedMilliseconds} ms");
            if (!isValid)
            {
                return Unauthorized("Invalid username or password.");
            }
           
            return Ok(new 
            { 
                message = "Login successful.", 
                username = user.Username,
                token = createToken(user)
            });
        }

        private string createToken(User user)
        {
            // Implement JWT token creation logic here
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryInMinutes"])),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
