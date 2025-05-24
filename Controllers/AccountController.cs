using exomine.Data;
using exomine.Data.Models;
using exomine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
    private readonly MineContext _db;

    public AccountController(MineContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
        if (user == null)
        {
            model.ErrorMessage = "Invalid username.";
            return View(model);
        }
        if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        {
            model.ErrorMessage = "Invalid password.";
            return View(model);
        }
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        if (model.Password != model.ConfirmPassword)
        {
            model.ErrorMessage = "Passwords must match.";
            return View(model);
        }

        if (await _db.Users.AnyAsync(u => u.Username == model.Username))
        {
            model.ErrorMessage = "Username taken.";
            return View(model);
        }

        var user = new User
        {
            Username = model.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
        };
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
