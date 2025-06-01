using exomine.Data;
using exomine.Data.Models;
using exomine.Models;
using exomine.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exomine.Controllers
{
    public class GeneratorController : Controller
    {
        GeneratorService _gen;
        MineContext _db;
        public GeneratorController(MineContext mineContext, GeneratorService generatorService)
        {
            _db = mineContext;
            _gen = generatorService;
        }
        [HttpGet]
        public IActionResult New()
        {
            return View(new NewGameViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> New(NewGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int? userid = HttpContext.Session.GetInt32("UserId");
            User? user = null;
            if (userid != null) user = await _db.Users.Where(u => u.Id == userid).FirstOrDefaultAsync();
            if (user == null) return RedirectToAction("Login", "Account");
            Game? game = null;
            if (model.UseExisting)
            {
                game = await _gen.GetGame(model.Size, model.Type, model.Difficulty, user);
                if (game == null)
                {
                    model.ErrorMessage = "Could not find board. Try a different type, try generating a board, or try again later.";
                    return View(model);
                }
            }
            else
            {
                game = _gen.GenerateRandom(model.Size, model.Type);
                _db.Games.Add(game);
                await _db.SaveChangesAsync();
                if (game == null)
                {
                    model.ErrorMessage = "Generation error. Use \"Load from Database\" or try again later.";
                    return View(model);
                }
            }
            if (game == null) return View(model);
            return RedirectToAction("Play", "Game", new { id = game.Id });
        }

    }
}