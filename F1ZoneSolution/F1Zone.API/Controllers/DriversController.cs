using F1Zone.API.INTERFACE;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{name}")]
        public async Task<ActionResult<Drivers>> GetDriverByName(string name)
        {
            var searchName = name.Replace("-", " ");

            // Ha a _service nem találja, próbáld meg lekérni az összeset és ott szűrni
            var drivers = await _service.GetAll();
            var result = drivers.FirstOrDefault(d => d.driver_name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
