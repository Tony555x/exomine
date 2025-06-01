using exomine.Data;
using exomine.Data.Models;
using exomine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace exomine.Services
{
    public class AuthService
    {
        private readonly MineContext _db;

        public AuthService(MineContext db)
        {
            _db = db;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;
            return user;
        }

        public async Task<(User? user, string? Error)> RegisterUserAsync(RegisterViewModel model)
        {
            if (await _db.Users.AnyAsync(u => u.Username == model.Username))
                return (null, "Username taken.");

            var user = User.CreateNew(model.Username, model.Password);
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return (user, null);
        }
    }
}
