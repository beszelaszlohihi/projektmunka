using F1ZoneLibrary.DATA;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F1Zone.API.Controllers
{
    [Route("api/homepagesettings")]
    [ApiController]
    public class HomepageSettingsController : ControllerBase
    {
        private readonly F1ZoneDbContext _context;

        public HomepageSettingsController(F1ZoneDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HomepageSettings>> GetById(int id)
        {
            var settings = await _context.HomepageSettings.FindAsync(id);

            if (settings == null)
                return NotFound();

            return Ok(settings);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HomepageSettings model)
        {
            if (id != model.Id)
                return BadRequest("Az ID nem egyezik.");

            var existing = await _context.HomepageSettings.FindAsync(id);

            if (existing == null)
                return NotFound("A homepage beállítás nem található.");

            existing.FeaturedCircuitId = model.FeaturedCircuitId;
            existing.FeaturedDriverId = model.FeaturedDriverId;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}