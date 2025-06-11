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
    }
}
