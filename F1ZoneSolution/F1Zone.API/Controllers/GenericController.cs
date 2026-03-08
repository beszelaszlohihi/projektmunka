using F1Zone.API.INTERFACE;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : ControllerBase where T : class
    {
        private readonly IGenericF1ZoneService<T> _service;

        public GenericController(IGenericF1ZoneService<T> service)
        {
            _service = service;
        }

        //osszes lekerdezese
        [HttpGet]
        public async Task<ActionResult<List<T>>> GetAll()
        {
            return await _service.GetAll();
        }

        //id alapjan
        [HttpGet("{id:int}")]
        public async Task<ActionResult<T>> GetById(int id)
        {
            var entity = await _service.GetById(id);
            return Ok(entity);
        }

        //uj letrehozasa
        [HttpPost]
        public async Task<ActionResult<T>> Create([FromBody] T entity)
        {
            var created = await _service.Add(entity);
            return Ok(created);
        }

        //frissitese
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] T entity)
        {
            await _service.Update(entity);
            return NoContent();
        }

        //torlese
        [HttpDelete]
        public async Task<ActionResult> Delete(T entity)
        {
            await _service.Delete(entity);
            return NoContent();
        }
    }
}
