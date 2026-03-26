using F1Zone.API.INTERFACE;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Fastest_lapsController : GenericController<Fastest_laps>
    {
        public Fastest_lapsController(IGenericF1ZoneService<Fastest_laps> service) : base(service) { }
    }
}
