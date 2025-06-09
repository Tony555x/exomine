using exomine.Data;
using exomine.Data.Models;
using exomine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
public class AchievementsController : Controller
{
    private readonly MineContext _db;

    public AchievementsController(MineContext context)
    {
        _db = context;
    }

    public async Task<IActionResult> Index()
    {
        int? userid = HttpContext.Session.GetInt32("UserId");
        User? user = null;
        if (userid != null) user = await _db.Users.Where(u => u.Id == userid).FirstOrDefaultAsync();
        if (user == null) return RedirectToAction("Login", "Account");

        var userAchievementIds = (await _db.UserAchievements
            .Where(ua => ua.UserId == userid)
            .Select(ua => ua.AchievementId)
            .ToListAsync()).ToHashSet();

        var achievements = await _db.Achievements
            .OrderBy(a => a.Id)
            .ToListAsync();

        var viewModel = new AllAchievementsViewModel
        {
            Achievements = achievements.Select(a => new AllAchievementsViewModel.AchievementDisplay
            {
                Name = a.Name,
                IsUnlocked = userAchievementIds.Contains(a.Id),
            }).ToList()
        };

        return View(viewModel);
    }
}