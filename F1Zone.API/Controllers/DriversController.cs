using F1Zone.API.INTERFACE;
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

        public DriversController(IGenericF1ZoneService<Drivers> service) : base(service)
        {
            _service = service;
        }

        // ÚJ METÓDUS: Név alapján történő lekérés
        // Így az elérési útja ez lesz: api/drivers/by-name/Lewis-Hamilton
        [HttpGet("{name}")]
        public async Task<ActionResult<Drivers>> GetDriverByName(string name)
        {
            var searchName = name.Replace("-", " ");
            var drivers = await _service.GetAll();
            var result = drivers.FirstOrDefault(d => d.driver_name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("admin-list")]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetDriversForAdmin()
        {
            var drivers = await _service.GetAll();

            // Itt alakítjuk át a nyers adatokat DTO-vá
            var result = drivers.Select(d => new DriverDto
            {
                driver_id = d.driver_id,
                driver_name = d.driver_name,
                wins = d.wins,
                championships = d.championships,
                points = d.points,
                // EZEK HIÁNYOZTAK EDDIG:
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

        //if (id != driverDto.driver_id) return BadRequest("ID mismatch")
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver(int id, [FromBody] DriverDto driverDto)
        {
            var driver = await _service.GetById(id);
            if (driver == null) return NotFound("A pilóta nem található.");

            // Átadjuk az ÖSSZES módosítható adatot
            driver.driver_name = driverDto.driver_name; // Akár a nevét is módosíthatod
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

            // Szűrünk azokra, akiknek a TeamName megegyezik (kis-nagybetű nem számít)
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
