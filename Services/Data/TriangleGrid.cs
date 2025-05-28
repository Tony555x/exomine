using exomine.Data.Models;

namespace exomine.Services.Data
{
    public class TriangleGrid : Grid
    {
        public TriangleGrid(int size) : base(size) { }
        public Tile[,] TileGrid { get; set; } = new Tile[0, 0];
        public override void CreateGrid()
        {
            Bombs = Size * Size * 2 / 5;
            RemainingBombs = Bombs;
            Width = Size;
            Height = Size;
            TileGrid = new Tile[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    TileGrid[x, y] = new Tile();
                    Tiles.Add(TileGrid[x, y]);
                }
            }
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int dx = -2; dx <= 2; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (((x + y) % 2) == 0 && dy == -1 && Math.Abs(dx) > 1) continue;
                            if (((x + y) % 2) == 1 && dy == 1 && Math.Abs(dx) > 1) continue;
                            if (x + dx >= 0 && x + dx < Width && y + dy >= 0 && y + dy < Height && (dx != 0 || dy != 0))
                            {
                                TileGrid[x, y].Adj.Add(TileGrid[x + dx, y + dy]);
                                TileGrid[x, y].Empty++;
                            }
                        }
                    }
                    Console.Write(TileGrid[x, y].Empty);
                }
                Console.WriteLine();
            }
        }
        public override Game Compress()
        {
            Game game = new Game();
            game.Type = exomine.Data.Enums.GridType.Triangle;
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
            return game;
        }
    }
}