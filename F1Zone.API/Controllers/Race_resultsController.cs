using F1Zone.API.INTERFACE;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Race_resultsController : GenericController<Race_results>
    {
        public Race_resultsController(IGenericF1ZoneService<Race_results> service) : base(service) { }

    }
}
