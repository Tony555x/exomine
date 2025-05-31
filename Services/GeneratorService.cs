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
            if (type == GridType.SquareTriHex)
            {
                grid = new SquareTriHexGrid(size);
            }
            grid.Init();
            while (true)
            {
                int dif = TrySolve(grid);
                if (dif > -1) break;
                Tile? t = grid.Tiles.Where(t => t.Revealable && !t.Known && t.Adj.Any(t2 => !t2.Revealable)).FirstOrDefault();
                if (t != null)
                {
                    t.Known = true;
                    continue;
                }
                t = grid.Tiles.Where(t => !t.Revealable && !t.Bomb).FirstOrDefault();
                if (t == null)
                {
                    continue;
                }
                grid.RevealTile(t, true);
            }
            for (int i = 0; i < grid.Tiles.Count; i++)
            {
                Tile t = grid.Tiles[i];
                if (!t.Revealed) continue;
                grid.UnrevealTile(t, true);
                grid.Clear();
                int dif = TrySolve(grid);
                if (dif==-1)
                {
                    grid.RevealTile(t, true);
                }
                //else Console.WriteLine("Trim Reveal");
            }
            for (int i = 0; i < grid.Tiles.Count; i++)
            {
                Tile t = grid.Tiles[i];
                if (!t.Known||t.Bomb) continue;
                t.Known = false;
                grid.Clear();
                int dif = TrySolve(grid);
                if (dif==-1)
                {
                    t.Known = true;
                }
                //else Console.WriteLine("Trim Known");
            }
            grid.Clear();
            int fdif = TrySolve(grid);
            Game game = grid.Compress();
            game.Difficulty = fdif;
            return game;
        }
        int TrySolve(IGrid grid)
        {
            int dif = 0;
            int d = SolveStep(grid);
            while (d != -1)
            {
                dif += d;
                d = SolveStep(grid);
            }
            if (grid.RevealableTiles == grid.Tiles.Count) return dif;
            else return -1;
        }
        int SolveStep(IGrid grid) // return step difficulty
        {
            if (grid.RemainingTiles == 0) return -1;
            if (grid.RemainingBombs == grid.RemainingTiles)
            {
                //Console.WriteLine("BombFill: "+grid.RemainingBombs);
                for (int i = 0; i < grid.Tiles.Count; i++)
                {
                    Tile t = grid.Tiles[i];
                    if (t.Revealable == false)
                    {
                        grid.RevealTile(t, false);
                    }
                }
                return 0;
            }
            if (grid.RemainingBombs == 0)
            {
                //Console.WriteLine("ClearFill: "+grid.RemainingTiles);
                for (int i = 0; i < grid.Tiles.Count; i++)
                {
                    Tile t = grid.Tiles[i];
                    if (t.Revealable == false)
                    {
                        grid.RevealTile(t, false);
                    }
                }
                return 0;
            }
            for (int i = 0; i < grid.Tiles.Count; i++) // Chord
            {
                Tile t = grid.Tiles[i];
                if (!t.Known||!t.Revealable||t.Bomb||t.Empty == 0) continue;
                if (t.RemainingBombs == 0||t.RemainingBombs==t.Empty)
                {
                    for (int j = 0; j < t.AdjCount; j++)
                    {
                        Tile t2 = t.Adj[j];
                        if (!t2.Revealable) grid.RevealTile(t2, false);
                    }
                    return 0;
                }
            }
            for (int i = 0; i < grid.Tiles.Count; i++)
            {
                Tile t = grid.Tiles[i];
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
                if (grid.RemainingTiles < 20)
                {
                    for (int j = 0; j < grid.Tiles.Count; j++)
                    {
                        if (!grid.Tiles[j].Revealable && !grid.Tiles[j].Lock) rel.Add(grid.Tiles[j]);
                    }
                }
                for (int j = 0; j < grid.Tiles.Count; j++) grid.Tiles[j].Lock = false;
                if (!Attempt(rel, 0, true, grid.RemainingBombs - 1))
                {
                    //Console.WriteLine("Clear: " + (i + 1) + " " + (i + 1) + " rel: " + rel.Count);
                    grid.RevealTile(t, false);
                    return 1;
                }
                //Console.WriteLine();
                if (!Attempt(rel, 0, false, grid.RemainingBombs))
                {
                    //Console.WriteLine("Bomb: " + (i + 1) + " " + (i + 1) + " rel: " + rel.Count);
                    grid.RevealTile(t, false);
                    return 1;
                }
                //Console.WriteLine();
            }
            return -1;
        }
        bool Attempt(List<Tile> rel, int i, bool val, int rem)
        {
            //Console.Write(i);
            if (rem < 0) return false;
            if (i > 20) return true; //i hope this isnt a bad idea
            Tile t = rel[i];
            bool ok = true, sol = false;
            ok = t.SetBomb(val);
            if (ok && i == rel.Count - 1) sol = true;
            if (ok && !sol) sol = sol || Attempt(rel, i + 1, false, rem);
            if (ok && !sol) sol = sol || Attempt(rel, i + 1, true, rem - 1);
            t.Clear();
            return sol;
        }
    }
}