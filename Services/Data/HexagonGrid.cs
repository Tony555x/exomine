using exomine.Data.Models;

namespace exomine.Services.Data
{
    public class HexagonGrid : Grid
    {
        public HexagonGrid(int size) : base(size) { }

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
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (x % 2 == 0 && dy == 1 && dx != 0) continue;
                            if (x % 2 == 1 && dy == -1 && dx != 0) continue;
                            if (x + dx >= 0 && x + dx < Width && y + dy >= 0 && y + dy < Height && (dx != 0 || dy != 0))
                            {
                                TileGrid[x, y].Adj.Add(TileGrid[x + dx, y + dy]);
                                TileGrid[x, y].Empty++;
                            }
                        }
                    }
                }
            }
        }
        public override Game Compress()
        {
            Game game = new Game();
            game.Type = exomine.Data.Enums.GridType.Hexagon;
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