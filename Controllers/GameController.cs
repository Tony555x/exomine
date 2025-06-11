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
            int? userid = HttpContext.Session.GetInt32("UserId");
            User? user = null;
            if (userid != null) user = await _db.Users.Where(u => u.Id == userid).FirstOrDefaultAsync();
            if (user == null) return RedirectToAction("Login", "Account");
            Game? game = await _db.Games.Where(g => g.Id == id).FirstOrDefaultAsync();
            if (game == null)
            {
                return RedirectToAction("New", "Generator");
            }
            GameViewModel gvm = new()
            {
                Game = game,
                GameId = id
            };
            return View(gvm);
        }
        [HttpPost]
        [Route("Game/Play")]
        public async Task<IActionResult> Play(GameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int? userid = HttpContext.Session.GetInt32("UserId");
            User? user = null;
            if (userid != null) user = await _db.Users.Where(u => u.Id == userid).FirstOrDefaultAsync();
            if (user == null) return RedirectToAction("Login", "Account");
            UserGame? ug = await _db.UserGames.Where(ug => ug.UserId == userid && ug.GameId == model.GameId).FirstOrDefaultAsync();
            if (ug == null)
            {
                ug = new UserGame
                {
                    UserId = user.Id,
                    GameId = model.GameId,
                    Win = false

                };
                await _db.UserGames.AddAsync(ug);
            }
            ug.Win = true;
            if (model.Time.HasValue)
            {
                ug.Time = model.Time.Value;
            }
            var relevantAchievements = await _db.Achievements
                .Where(a =>
                    (a.GridType == model.Game.Type || a.GridType == null) &&
                    (a.MinSize == null || model.Game.Size >= a.MinSize) &&
                    (a.MinDifficulty == null || model.Game.Difficulty >= a.MinDifficulty) &&
                    (a.MaxTimeSeconds == null || (model.Time.HasValue && model.Time.Value.TotalSeconds <= a.MaxTimeSeconds))
                ).ToListAsync();
            Console.WriteLine("size: "+model.Game.Size);
            Console.WriteLine("rel: "+relevantAchievements.Count);
            var userAchievementIds = await _db.UserAchievements
                .Where(ua => ua.UserId == userid)
                .Select(ua => ua.AchievementId)
                .ToHashSetAsync();
            foreach (var achievement in relevantAchievements)
            {
                if (!userAchievementIds.Contains(achievement.Id))
                {
                    await _db.UserAchievements.AddAsync(new UserAchievement
                    {
                        UserId = userid.Value,
                        AchievementId = achievement.Id
                    });
                }
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("New", "Generator");
        }

    }
}