using exomine.Data;
using exomine.Data.Enums;
using exomine.Data.Models;
using exomine.Models;
using Microsoft.EntityFrameworkCore;

namespace exomine.Services
{
    public class GeneratorService
    {
        MineContext _db;
        public GeneratorService(MineContext mineContext)
        {
            _db = mineContext;
        }
        public async Task<Game> GetGame(int size, GridType type, int difficulty, User user)
        {
            Game game = await _db.Games.Where(g =>
                g.Type == type &&
                g.Size == size &&
                g.Difficulty >= difficulty &&
                _db.UserGames.Any(ug => ug.GameId == g.Id && ug.UserId == user.Id)
                ).OrderBy(g => g.Difficulty).FirstAsync();
            if (game != null) return game;
            return GenerateMin(size, type, difficulty);
        }
        Game GenerateMin(int size, GridType type, int difficulty)
        {
            Game ng = GenerateRandom(size, type, difficulty);
            return ng;
        }
        Game GenerateRandom(int size, GridType type, int difficulty)
        {
            int[,] grid;
            return new Game();
            if (type == GridType.Triangle)
            {
                //grid
            }
        }
    }
}