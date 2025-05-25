namespace exomine.Services.Data
{
    public interface IGrid
    {
        public int Size { get; set; }
        public int Mines{ get; set; }
        public int RevealCount { get; set; }
        public Tile[,] Tiles { get; set; } //X,Y
        public List<Tile> TileList{ get; set; }
        public int Width { get; set; }
        public int Height{ get; set; }
        public void Init();
    }
}