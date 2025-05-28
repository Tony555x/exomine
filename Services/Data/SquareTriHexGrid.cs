using exomine.Data.Models;

namespace exomine.Services.Data
{
    public class SquareTriHexGrid : Grid
    {
        public SquareTriHexGrid(int size) : base(size) { }

        public Tile[,] TileGrid { get; set; } = new Tile[0, 0];
        public Tile[,] TileGridAlt { get; set; } = new Tile[0, 0];
        public override void CreateGrid()
        {
            Width = Size - (Size + 1) % 2;
            Height = Size - (Size + 1) % 2;
            TileGrid = new Tile[Width, Height];
            TileGridAlt = new Tile[Width / 2, Height - 1];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    TileGrid[x, y] = new Tile();
                    Tiles.Add(TileGrid[x, y]);
                }
            }
            //Console.WriteLine("MainGrid ok");
            for (int y = 0; y < Height - 1; y++)
            {
                for (int x = 0; x < Width / 2; x++)
                {
                    TileGridAlt[x, y] = new Tile();
                    Tiles.Add(TileGridAlt[x, y]);
                }
            }
            //Console.WriteLine("AltGrid ok");
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (x % 4 == 0 && dy == 1 && dx != 0) continue;
                            if (x % 4 == 2 && dy == -1 && dx != 0) continue;
                            if (x % 4 == 1 && dy == 1 && dx > 0) continue;
                            if (x % 4 == 1 && dy == -1 && dx < 0) continue;
                            if (x % 4 == 3 && dy == 1 && dx < 0) continue;
                            if (x % 4 == 3 && dy == -1 && dx > 0) continue;
                            if (x + dx >= 0 && x + dx < Width && y + dy >= 0 && y + dy < Height && (dx != 0 || dy != 0))
                            {
                                TileGrid[x, y].Adj.Add(TileGrid[x + dx, y + dy]);
                                TileGrid[x, y].Empty++;
                            }
                        }
                    }
                    //Console.Write(TileGrid[x, y].Empty);
                }
                //Console.WriteLine();
            }
            //Console.WriteLine("MainGrid Adj ok");
            for (int x = 0; x < Width / 2; x++)
            {
                for (int y = 0; y < Height - 1; y++)
                {
                    int v = y - y % 2 + 1;
                    int h = x * 2 + 1;
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (x % 2 == 0 && y % 2 == 0 && dx + dy > 0) continue;
                            if (x % 2 == 1 && y % 2 == 0 && -dx + dy > 0) continue;
                            if (x % 2 == 0 && y % 2 == 1 && -dx - dy > 0) continue;
                            if (x % 2 == 1 && y % 2 == 1 && dx - dy > 0) continue;
                            if (h + dx >= 0 && h + dx < Width && v + dy >= 0 && v + dy < Height)
                            {
                                TileGridAlt[x, y].Adj.Add(TileGrid[h + dx, v + dy]);
                                TileGridAlt[x, y].Empty++;
                                TileGrid[h + dx, v + dy].Adj.Add(TileGrid[x, y]);
                                TileGrid[h + dx, v + dy].Empty++;
                            }

                        }
                    }

                }
            }
            Bombs = Tiles.Count * 2 / 5;
            RemainingBombs = Bombs;
            //Console.WriteLine("AltGrid Adj ok");
        }
        public override Game Compress()
        {
            Game game = new Game();
            game.Type = exomine.Data.Enums.GridType.SquareTriHex;
            game.Size = Size;
            game.Difficulty = 100;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    game.Bombs += TileGrid[x, y].Bomb ? '1' : '0';
                    game.Revealed += TileGrid[x, y].Revealed ? '1' : '0';
                    game.Known += TileGrid[x, y].Known ? '1' : '0';
                }
            }
            for (int y = 0; y < Height-1; y++)
            {
                for (int x = 0; x < Width/2; x++)
                {
                    game.Bombs += TileGridAlt[x, y].Bomb ? '1' : '0';
                    game.Revealed += TileGridAlt[x, y].Revealed ? '1' : '0';
                    game.Known += TileGridAlt[x, y].Known ? '1' : '0';
                }
            }
            Bombs = Tiles.Count * 2 / 5;
            RemainingBombs = Bombs;
            return game;
        }
    }
}