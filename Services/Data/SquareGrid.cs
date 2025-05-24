namespace exomine.Services.Data
{
    public class SquareGrid : IGrid
    {
        public SquareGrid(int size)
        {
            Size = size;
            Mines = size * size * 2 / 5;
        }
        public Tile[,] Tiles { get; set; } = new Tile[0, 0];
        public int Mines{ get; set; }
        public int Size { get; set; }
        public int Width{ get; set; }
        public int Height{ get; set; }
        public List<Tile> TileList { get; set; } = new List<Tile>();
        public void Init()
        {
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
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (x + dx >= 0 && x + dx < Width && y + dy >= 0 && y + dy < Height)
                            {
                                Tiles[x, y].Adj.Add(Tiles[x + dx, y + dy]);
                            }
                        }
                    }
                }
            }
            Random rng = new Random();
            var shuffled = TileList.OrderBy(x => rng.Next()).ToList();
            for (int i = 0; i < Mines; i++)
            {
                Tile t = shuffled[i];
                t.Bomb = true;
                for (int j = 0; j < t.Adj.Count; j++)
                {
                    Tile t2 = t.Adj[j];
                    t2.Bombs++;
                }
            }
        }
    }
}