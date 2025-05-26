using exomine.Data;
using exomine.Data.Models;
using exomine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exomine.Controllers
{
    public class GameController : Controller
    {
        MineContext _db;
        public GameController(MineContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("Game/Play/{id}")]
        public async Task<IActionResult> Play(int id)
        {
            Game? game = await _db.Games.Where(g => g.Id == id).FirstOrDefaultAsync();
            if (game == null)
            {
                return RedirectToAction("New", "Generator");
            }
            GameViewModel gvm = new()
            {
                Game = game
            };
            return View(gvm);
        }
    }
}