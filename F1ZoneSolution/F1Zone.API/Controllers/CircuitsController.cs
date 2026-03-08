using F1Zone.API.INTERFACE;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircuitsController : GenericController<Circuits>
    {
        public CircuitsController(IGenericF1ZoneService<Circuits> service) : base(service) { }
    }
}
