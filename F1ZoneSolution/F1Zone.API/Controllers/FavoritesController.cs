using F1Zone.API.INTERFACE;
using F1ZoneLibrary.DATA;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F1Zone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : GenericController<UserFavorites>
    {
        private readonly F1ZoneDbContext _context; 

        public FavoritesController(IGenericF1ZoneService<UserFavorites> service, F1ZoneDbContext context)
            : base(service)
        {
            _context = context;
        }

        // EGYEDI TÖRLÉS: userId és itemId alapján
        [HttpDelete("{userId}/{driverId}")]
        public async Task<ActionResult> DeleteFavorite(int userId, int driverId)
        {
            var fav = await _context.UserFavorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ItemId == driverId);

            if (fav == null) return NotFound();

            _context.UserFavorites.Remove(fav);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
