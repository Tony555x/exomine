using exomine.Data;
using exomine.Data.Models;
using exomine.Models;
using exomine.Services;
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
        model.UsernameErrorMessage = String.Empty;
        model.PasswordErrorMessage = String.Empty;
        if (!ModelState.IsValid) return View(model);
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
        if (user == null)
        {
            model.UsernameErrorMessage = "Username not found.";
            return View(model);
        }
        if (model.Password==null||!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        {
            model.PasswordErrorMessage = "Incorrect password.";
            return View(model);
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetString("Role", user.Role);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        model.UsernameErrorMessage = String.Empty;
        model.PasswordErrorMessage = String.Empty;
        model.ConfirmErrorMessage = String.Empty;
        if (!ModelState.IsValid) return View(model);
        if (model.Username == null || model.Username.Length < 1 || model.Username.Length > 20)
        {
            model.UsernameErrorMessage = "Username length must be between 1 and 20.";
            return View(model);
        }
        if (await _db.Users.AnyAsync(u => u.Username == model.Username))
        {
            model.UsernameErrorMessage = "Username already taken.";
            return View(model);
        }
        if (model.Password == null || model.Password.Length < 8 || model.Password.Length > 50)
        {
            model.PasswordErrorMessage = "Password length must be between 8 and 50.";
            return View(model);
        }
        if (model.ConfirmPassword == null || model.Password != model.ConfirmPassword)
        {
            model.ConfirmErrorMessage = "Passwords must match.";
            return View(model);
        }

        var user = exomine.Data.Models.User.CreateNew(model.Username, model.Password);
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetString("Role", user.Role);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
