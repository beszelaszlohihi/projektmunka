using F1Zone.API.INTERFACE;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Team_sponsorsController : GenericController<Team_sponsors>
    {
        public Team_sponsorsController(IGenericF1ZoneService<Team_sponsors> service) : base(service) { }
    }
}
