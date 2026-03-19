using F1ZoneLibrary.DATA;
using F1ZoneLibrary.Dto;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly F1ZoneDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(F1ZoneDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            //letezik e az email
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                return BadRequest("Ez az email cím már foglalt!");
            }

            //Jelszó titkosítása
            
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Új felhasználó objektum létrehozása
            var user = new User
            {
                Email = model.Email,
                Username = model.Username,
                PasswordHash = passwordHash,
                Role = model.Role // Itt dől el, hogy Admin vagy User
            };

            // Mentés az adatbázisba
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Sikeres regisztráció!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            //Megkeressük a felhasználót az email alapján
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            //Ha nincs ilyen email, vagy a jelszó NEM egyezik a tárolt hash-sel
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return Unauthorized("Hibás email vagy jelszó!");
            }

            
            var token = CreateToken(user);

            return Ok(new { Token = token, Username = user.Username, Role = user.Role });
        }


        //createtoken metodus letrehozasa
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role) // Ez donti el hogy Admin vagy User
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Secret").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), // 1 napig érvényes a belépés
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }

    
}
