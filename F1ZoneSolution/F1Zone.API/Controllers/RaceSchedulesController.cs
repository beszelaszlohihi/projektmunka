using F1Zone.API.INTERFACE;
using F1ZoneLibrary.DATA;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceSchedulesController : GenericController<RaceSchedules>
    {
        private readonly F1ZoneDbContext _context;

        public RaceSchedulesController(
            IGenericF1ZoneService<RaceSchedules> service,
            F1ZoneDbContext context
        ) : base(service)
        {
            _context = context;
        }

        [HttpGet("next-race")]
        public async Task<ActionResult<RaceSchedules>> GetNextRace()
        {
            var nextRace = await _context.RaceSchedules
                .Where(r => r.RaceDate > DateTime.Now)
                .OrderBy(r => r.RaceDate)
                .FirstOrDefaultAsync();

            if (nextRace == null)
                return NotFound("Nincs közelgő futam.");

            return Ok(nextRace);
        }
    }
}