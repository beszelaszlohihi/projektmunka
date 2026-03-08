using F1Zone.API.INTERFACE;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Driver_contractsController : GenericController<Driver_contracts>
    {
        public Driver_contractsController(IGenericF1ZoneService<Driver_contracts> service) : base(service) { }
    }
}
