using F1ZoneLibrary.DATA;
using F1ZoneLibrary.Dto;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly F1ZoneDbContext _context;

        public AuthController(F1ZoneDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            // 1. Ellenőrizzük, létezik-e már az email
            if (await _context.User.AnyAsync(u => u.Email == model.Email))
            {
                return BadRequest("Ez az email cím már foglalt!");
            }

            // 2. Jelszó titkosítása BCrypt-tel
            // Ez a sor csinál a "Jelszo123"-ból egy olvashatatlan katyvaszt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // 3. Új felhasználó objektum létrehozása
            var user = new User
            {
                Email = model.Email,
                Username = model.Username,
                PasswordHash = passwordHash,
                Role = model.Role // Itt dől el, hogy "Admin" vagy "User"
            };

            // 4. Mentés az adatbázisba
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Sikeres regisztráció!");
        }
    }
}
