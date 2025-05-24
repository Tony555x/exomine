using exomine.Models;
using Microsoft.AspNetCore.Mvc;

namespace exomine.Controllers
{
    public class GeneratorController : Controller
    {
        [HttpGet]
        public IActionResult New()
        {
            return View(new NewGameViewModel());
        }

        [HttpPost]
        public IActionResult New(NewGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Play", "Game", new { id = 1 });
        }

    }
}