using exomine.Data.Models;

namespace exomine.Services.Data
{
    public interface IGrid
    {
        public int Size { get; set; }
        public int Bombs { get; set; }
        public int RevealableTiles { get; set; }
        public int RemainingTiles { get; }
        public int RemainingBombs { get; set; }
        //public Tile[,] TileGrid { get; set; }
        public List<Tile> Tiles { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public void Init();
        public void RevealTile(Tile t, bool perm);
        public void UnrevealTile(Tile t, bool perm);
        public void Clear();
        public Game Compress();
    }
}