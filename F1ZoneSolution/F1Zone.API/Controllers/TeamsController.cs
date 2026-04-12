using F1Zone.API.INTERFACE;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : GenericController<Teams>
    {
        public TeamsController(IGenericF1ZoneService<Teams> service) : base(service) { }
    }
}
