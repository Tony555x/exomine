namespace exomine.Services.Data
{
    public class HexagonGrid : Grid
    {
        public HexagonGrid(int size) : base(size) { }
        public override void CreateGrid()
        {
            Bombs = Size * Size * 2 / 5;
            RemainingBombs = Bombs;
            Width = Size;
            Height = Size;
            Tiles = new Tile[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Tiles[x, y] = new Tile(x, y);
                    TileList.Add(Tiles[x, y]);
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
                                Tiles[x, y].Adj.Add(Tiles[x + dx, y + dy]);
                                Tiles[x, y].Empty++;
                            }
                        }
                    }
                    Console.Write(Tiles[x, y].Empty);
                }
                Console.WriteLine();
            }
        }
    }
}