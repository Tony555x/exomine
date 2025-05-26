namespace exomine.Services.Data
{
    public abstract class Grid : IGrid
    {
        public Grid(int size)
        {
            Size = size;
        }
        public Tile[,] Tiles { get; set; } = new Tile[0, 0];
        public int Bombs{ get; set; }
        public int Size { get; set; }
        public int RevealableTiles{ get; set; }
        public int RemainingTiles{ get{ return TileList.Count - RevealableTiles; } }
        public int RemainingBombs{ get; set; }
        public int Width { get; set; }
        public int Height{ get; set; }
        public List<Tile> TileList { get; set; } = new List<Tile>();

        public void Init()
        {
            CreateGrid();
            ScatterMines();
        }
        public abstract void CreateGrid();
        void ScatterMines()
        {
            
            Random rng = new Random();
            TileList = TileList.OrderBy(x => rng.Next()).ToList();
            for (int i = 0; i < Bombs; i++)
            {
                Tile t = TileList[i];
                t.Bomb = true;
                for (int j = 0; j < t.Adj.Count; j++)
                {
                    Tile t2 = t.Adj[j];
                    t2.Bombs++;
                    t2.Empty--;
                }
            }
        }
        public void RevealTile(Tile t, bool perm)
        {
            t.Reveal(perm);
            RevealableTiles++;
            if (t.Bomb) RemainingBombs--;
        }
        public void RevealTile(int x, int y, bool perm)
        {
            RevealTile(Tiles[x, y], perm);
        }
    }
}