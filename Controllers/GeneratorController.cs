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
        public GeneratorController(MineContext mineContext,GeneratorService generatorService)
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
            User user = await _db.Users.Where(u => u.Id == HttpContext.Session.GetInt32("UserId")).FirstOrDefaultAsync();
            if (user == null) return RedirectToAction("Login", "Account");
            Game game = null;
            if (model.UseExisting)
            {
                game = await _gen.GetGame(model.Size, model.Type, model.Difficulty, user);
            }
            else
            {
                game = _gen.GenerateRandom(model.Size, model.Type);
                _db.Games.Add(game);
                await _db.SaveChangesAsync();
            }
            GameViewModel gim=new GameViewModel();
            gim.Game = game;
            return RedirectToAction("Play", "Game", gim);
        }

    }
}