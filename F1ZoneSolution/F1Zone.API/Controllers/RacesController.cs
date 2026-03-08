using F1Zone.API.INTERFACE;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacesController : GenericController<Races>
    {
        public RacesController(IGenericF1ZoneService<Races> service) : base(service) { }
    }
}
