using exomine.Data;
using exomine.Data.Enums;
using exomine.Data.Models;
using exomine.Models;
using exomine.Services.Data;
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
        public async Task<Game?> GetGame(int size, GridType type, int difficulty, User user)
        {
            Game game = await _db.Games.Where(g =>
                g.Type == type &&
                g.Size == size &&
                g.Difficulty >= difficulty &&
                _db.UserGames.Any(ug => ug.GameId == g.Id && ug.UserId == user.Id)
                ).OrderBy(g => g.Difficulty).FirstAsync();
            return game;
        }
        public Game GenerateRandom(int size, GridType type)
        {
            IGrid? grid = new SquareGrid(size);
            if (type == GridType.Square)
            {
                grid = new SquareGrid(size);
            }
            grid.Init();
            while (true)
            {
                bool ok = TrySolve(grid);
                if (ok) break;
                Tile t = grid.TileList.Where(t => t.Revealable && !t.Known && t.Adj.Any(t2 => !t2.Revealable)).First();
                if (t != null)
                {
                    t.Known = true;
                    continue;
                }
                t = grid.TileList.Where(t => !t.Revealable && !t.Bomb).First();
                t.Revealed = true;
                t.Revealable = true;
            }
            Game game = new Game();
            game.Type = type;
            game.Size = size;
            game.Difficulty = 100;
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    game.Bombs += grid.Tiles[x, y].Bomb;
                    game.Revealed += grid.Tiles[x, y].Revealed;
                    game.Known += grid.Tiles[x, y].Known;
                }
            }
            return game;
        }
        bool TrySolve(IGrid grid)
        {
            bool ok = SolveStep(grid);
            while (ok) SolveStep(grid);
            if (grid.RevealCount == grid.TileList.Count) return true;
            else return false;
        }
        bool SolveStep(IGrid grid)
        {
            bool ok = false;
            for (int i = 0; i < grid.TileList.Count; i++)
            {
                Tile t = grid.TileList[i];
                if (t.Revealable) continue;
                if (!t.Adj.Any(t2 => t2.Known && t2.Revealable && !t2.Bomb)) continue;
                List<Tile> rel = new List<Tile>();
                rel.Add(t);
                t.Lock = true;
                for (int j = 0; j < rel.Count; j++) //BFS
                {
                    Tile t2 = rel[j];
                    for (int k = 0; k < t2.Adj.Count; k++)
                    {
                        Tile t3 = t2.Adj[k];
                        if (t3.Revealable) continue;
                        rel.Add(t3);
                        t3.Lock = true;
                    }
                }
                for (int j = 0; j < rel.Count; j++) rel[j].Lock = false;
                if (!Attempt(rel, 0, false))
                {
                    ok = true;
                    t.Revealable = true;
                    t.Known = true;
                    break;
                }
                if (!Attempt(rel, 0, true))
                {
                    ok = true;
                    t.Revealable = true;
                    break;
                }
            }
            return ok;
        }
        bool Attempt(List<Tile> rel, int i, bool val)
        {
            Tile t = rel[i];
            bool ok = true, sol = false;
            for (int j = 0; j < t.Adj.Count; j++)
            {
                Tile t2 = t.Adj[j];
                if (!t2.Known || !t2.Revealable || t2.Bomb) continue;
                t2.Empty--;
                if (val) t2.Current++;
                if (t2.Remaining < t2.Empty) ok = false;
                if (t2.Remaining < 0) ok = false;
            }
            if (ok && i == rel.Count - 1) sol = true;
            if (ok && !sol) sol = sol || Attempt(rel, i + 1, false);
            if (ok && !sol) sol = sol || Attempt(rel, i + 1, true);
            for (int j = 0; j < t.Adj.Count; j++)
            {
                Tile t2 = t.Adj[j];
                if (!t2.Known || !t2.Revealable || t2.Bomb) continue;
                t2.Empty++;
                if (val) t2.Current--;
            }
            return sol;
        }
    }
}