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
            if (type == GridType.Hexagon)
            {
                grid = new HexagonGrid(size);
            }
            if (type == GridType.Square)
            {
                grid = new SquareGrid(size);
            }
            if (type == GridType.Triangle)
            {
                grid = new TriangleGrid(size);
            }
            grid.Init();
            while (true)
            {
                bool ok = TrySolve(grid);
                if (ok) break;
                Tile? t = grid.TileList.Where(t => t.Revealable && !t.Known && t.Adj.Any(t2 => !t2.Revealable)).FirstOrDefault();
                if (t != null)
                {
                    t.Known = true;
                    continue;
                }
                t = grid.TileList.Where(t => !t.Revealable && !t.Bomb).FirstOrDefault();
                if (t == null)
                {
                    continue;
                }
                grid.RevealTile(t, true);
            }
            Game game = new Game();
            game.Type = type;
            game.Size = size;
            game.Difficulty = 100;
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    game.Bombs += grid.Tiles[x, y].Bomb ? '1' : '0';
                    game.Revealed += grid.Tiles[x, y].Revealed ? '1' : '0';
                    game.Known += grid.Tiles[x, y].Known ? '1' : '0';
                }
            }
            return game;
        }
        bool TrySolve(IGrid grid)
        {
            bool ok = SolveStep(grid);
            while (ok) ok = SolveStep(grid);
            if (grid.RevealableTiles == grid.TileList.Count) return true;
            else return false;
        }
        bool SolveStep(IGrid grid)
        {
            bool ok = false;
            if (grid.RemainingBombs == grid.RemainingTiles)
            {
                //Console.WriteLine("BombFill: "+grid.RemainingBombs);
                for (int i = 0; i < grid.TileList.Count; i++)
                {
                    Tile t = grid.TileList[i];
                    if (t.Revealable == false)
                    {
                        grid.RevealTile(t, false);
                    }
                }
                return false;
            }
            if (grid.RemainingBombs == 0)
            {
                //Console.WriteLine("ClearFill: "+grid.RemainingTiles);
                for (int i = 0; i < grid.TileList.Count; i++)
                {
                    Tile t = grid.TileList[i];
                    if (t.Revealable == false)
                    {
                        grid.RevealTile(t, false);
                    }
                }
                return false;
            }
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
                        if (t3.Revealable && t3.Known && !t3.Bomb && !t3.Lock)
                        {
                            for (int h = 0; h < t3.Adj.Count; h++)
                            {
                                Tile t4 = t3.Adj[h];
                                if (t4.Revealable || t4.Lock) continue;
                                rel.Add(t4);
                                t4.Lock = true;
                            }
                            t3.Lock = true;
                        }
                    }
                }
                for (int j = 0; j < grid.TileList.Count; j++) grid.TileList[j].Lock = false;
                if (!Attempt(rel, 0, false))
                {
                    //Console.WriteLine("Bomb: " + (t.X+1) + " " + (t.Y+1)+" rel: "+rel.Count);
                    ok = true;
                    grid.RevealTile(t, false);
                    break;
                }
                //Console.WriteLine();
                if (!Attempt(rel, 0, true))
                {
                    //Console.WriteLine("Clear: " + (t.X + 1) + " " + (t.Y + 1) + " rel: " + rel.Count);
                    ok = true;
                    grid.RevealTile(t, false);
                    break;
                }
                //Console.WriteLine();
            }
            return ok;
        }
        bool Attempt(List<Tile> rel, int i, bool val)
        {
            if (i > 20) return true;
            Tile t = rel[i];
            bool ok = true, sol = false;
            ok = t.SetBomb(val);
            if (ok && i == rel.Count - 1) sol = true;
            if (ok && !sol) sol = sol || Attempt(rel, i + 1, false);
            if (ok && !sol) sol = sol || Attempt(rel, i + 1, true);
            t.Clear();
            return sol;
        }
    }
}