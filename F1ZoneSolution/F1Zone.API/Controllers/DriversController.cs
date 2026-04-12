using F1Zone.API.INTERFACE;
using F1ZoneLibrary.DATA;
using F1ZoneLibrary.Dto;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace F1Zone.API.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriversController : GenericController<Drivers>
    {
        private readonly IGenericF1ZoneService<Drivers> _service;

        private readonly F1ZoneDbContext _context;

        public DriversController(IGenericF1ZoneService<Drivers> service, F1ZoneDbContext context) : base(service)
        {
            _service = service;
            _context = context;
        }

        

        [HttpGet("{name}")]
        public async Task<ActionResult<DriverDto>> GetDriverByName(string name)
        {
            var searchName = name.Replace("-", " ");

            //Összekötjük a pilótát a szerződéssel és a motorral
            var driverData = await _context.Drivers
                .Where(d => d.driver_name == searchName)
                .Select(d => new DriverDto
                {
                    driver_id = d.driver_id,
                    driver_name = d.driver_name,
                    wins = d.wins,
                    podiums = d.podiums,
                    fastest_laps = d.fastest_laps,
                    championships = d.championships,
                    points = d.points,
                    biography = d.biography,
                    nationality = d.nationality,
                    teamname = d.teamname,

                    // JOIN a driver_contracts táblához
                    salary_estimate = _context.Driver_contracts
                        .Where(c => c.driver_id == d.driver_id)
                        .Select(c => c.salary_estimate)
                        .FirstOrDefault(),

                    team_sponsors = _context.Team_sponsors
                        .Where(ts => ts.team_id == _context.Teams
                            .Where(t => t.team_name == d.teamname)
                            .Select(t => t.team_id)
                            .FirstOrDefault())
                        .Join(_context.Sponsors,
                              ts => ts.sponsor_id,
                              s => s.sponsor_id,
                              (ts, s) => s.sponsor_name)
                        .ToList(),

                    // JOIN a teams és engines táblákhoz
                    manufacturer = (from t in _context.Teams
                                   join e in _context.Engines on t.engine_id equals e.engine_id
                                   where t.team_name == d.teamname
                                   select e.manufacturer).FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (driverData == null) return NotFound();

            return Ok(driverData);
        }


        [HttpGet("admin-list")]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetDriversForAdmin()
        {
            var drivers = await _service.GetAll();

            
            var result = drivers.Select(d => new DriverDto
            {
                driver_id = d.driver_id,
                driver_name = d.driver_name,
                wins = d.wins,
                championships = d.championships,
                points = d.points,
                nationality = d.nationality,
                debut_year = d.debut_year,
                podiums = d.podiums,
                fastest_laps = d.fastest_laps,
                biography = d.biography,
                teamname = d.teamname,
                teamcolor = d.teamcolor,
                racing_number = d.racing_number
            });

            return Ok(result);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver(int id, [FromBody] DriverDto driverDto)
        {
            var driver = await _service.GetById(id);
            if (driver == null) return NotFound("A pilóta nem található.");

            
            driver.driver_name = driverDto.driver_name;
            driver.wins = driverDto.wins;
            driver.championships = driverDto.championships;
            driver.points = driverDto.points;
            driver.nationality = driverDto.nationality;
            driver.debut_year = driverDto.debut_year;
            driver.podiums = driverDto.podiums;
            driver.fastest_laps = driverDto.fastest_laps;
            driver.biography = driverDto.biography;

            try
            {
                await _service.Update(driver);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HIBA: {ex.Message}");
                return BadRequest("Hiba a mentés során.");
            }
        }


        [HttpGet("team/{teamName}")]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetDriversByTeam(string teamName)
        {
            var allDrivers = await _service.GetAll();

           
            var teamDrivers = allDrivers
                .Where(d => d.teamname != null && d.teamname.Equals(teamName, StringComparison.OrdinalIgnoreCase))
                .Select(d => new DriverDto
                {
                    driver_id = d.driver_id,
                    driver_name = d.driver_name,
                    wins = d.wins,
                    championships = d.championships,
                    points = d.points,
                    nationality = d.nationality,
                    biography = d.biography,
                    teamname = d.teamname
                });

            return Ok(teamDrivers);
        }

    }
}
