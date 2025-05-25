using exomine.Models;
using Microsoft.AspNetCore.Mvc;

namespace exomine.Controllers
{
    public class GameController : Controller
    {
        [HttpGet]
        public IActionResult Play(GameViewModel gim)
        {
            return View(gim);
        }
    }
}