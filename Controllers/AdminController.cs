using exomine.Data;
using exomine.Models;
using exomine.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult AdminGenerate()
        {
            return View(new AdminGenerateViewModel { Size = 5, Count = 10 });
        }

        [HttpPost]
        public async Task<IActionResult> AdminGenerate(AdminGenerateViewModel model)
        {
            for (int i = 0; i < model.Count; i++)
            {
                var game = _gen.GenerateRandom(model.Size, model.Type);
                await _db.Games.AddAsync(game);
            }

            await _db.SaveChangesAsync();
            model.Message = "Completed.";
            return View();
        }
    }
}