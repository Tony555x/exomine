using exomine.Data;
using exomine.Data.Models;
using exomine.Models;
using exomine.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exomine.Controllers
{
    public class AdminController : Controller
    {
        MineContext _db;
        GeneratorService _gen;
        public AdminController(MineContext db, GeneratorService gen) {
            _db = db;
            _gen = gen;
        }
        [HttpGet]
        public async Task<IActionResult> AdminGenerate()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            User? user = null;
            if (userid != null) user = await _db.Users.Where(u => u.Id == userid).FirstOrDefaultAsync();
            if (user == null) return RedirectToAction("Login", "Account");
            if (user.Role != "Admin") return RedirectToAction("Index", "Home");
            return View(new AdminGenerateViewModel { Size = 5, Count = 10 });
        }

        [HttpPost]
        public async Task<IActionResult> AdminGenerate(AdminGenerateViewModel model)
        {
            if (model.Size < 1) return View(model);
            Console.WriteLine(model.Size + " " + model.Type);
            List<Task> tasks = new();
            var games = await Task.WhenAll(
                Enumerable.Range(0, model.Count).Select(i =>
                    Task.Run(() =>
                    {
                        Game g=_gen.GenerateRandom(model.Size, model.Type);
                        Console.WriteLine(i);
                        return g;
                    })
                )
            );

            await _db.Games.AddRangeAsync(games);
            await _db.SaveChangesAsync();
            model.Message = "Completed.";
            return View(model);
        }
    }
}