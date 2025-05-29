using exomine.Data.Models;

namespace exomine.Services.Data
{
    public abstract class Grid : IGrid
    {
        public Grid(int size)
        {
            Size = size;
        }
        //public Tile[,] TileGrid { get; set; } = new Tile[0, 0];
        public int Bombs { get; set; }
        public int Size { get; set; }
        public int RevealableTiles { get; set; }
        public int RemainingTiles { get { return Tiles.Count - RevealableTiles; } }
        public int RemainingBombs { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Tile> Tiles { get; set; } = new List<Tile>();

        public void Init()
        {
            CreateGrid();
            ScatterMines();
        }
        public abstract void CreateGrid();
        void ScatterMines()
        {

            Random rng = new Random();
            List<Tile> RTiles = Tiles.OrderBy(x => rng.Next()).ToList();
            for (int i = 0; i < Bombs; i++)
            {
                Tile t = RTiles[i];
                t.Bomb = true;
                for (int j = 0; j < t.Adj.Count; j++)
                {
                    Tile t2 = t.Adj[j];
                    t2.Bombs++;
                }
            }
            /*for (int i = 0; i < Tiles.Count; i++)
            {
                Tile t = Tiles[i];
                Console.WriteLine(i + " : " + t.Bombs);
            }*/
        }
        public void RevealTile(Tile t, bool perm)
        {
            t.Reveal(perm);
            RevealableTiles++;
            if (t.Bomb) RemainingBombs--;
        }
        public void UnrevealTile(Tile t, bool perm)
        {
            t.Unreveal(perm);
            RevealableTiles--;
            if (t.Bomb) RemainingBombs++;
        }
        public void Clear()
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tile t = Tiles[i];
                if (t.Revealable && !t.Revealed)
                {
                    UnrevealTile(t, false);
                }
            }
        }
        public abstract Game Compress();
    }
}