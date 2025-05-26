namespace exomine.Services.Data
{
    public interface IGrid
    {
        public int Size { get; set; }
        public int Bombs { get; set; }
        public int RevealableTiles { get; set; }
        public int RemainingTiles { get; }
        public int RemainingBombs { get; set; }
        public Tile[,] Tiles { get; set; }
        public List<Tile> TileList { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public void Init();
        public void RevealTile(Tile t, bool perm);
        public void RevealTile(int x, int y, bool perm);
    }
}