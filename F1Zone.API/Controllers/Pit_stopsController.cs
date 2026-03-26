using F1Zone.API.INTERFACE;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pit_stopsController : GenericController<Pit_stops>
    {
        public Pit_stopsController(IGenericF1ZoneService<Pit_stops> service) : base(service) { }
    }
}
