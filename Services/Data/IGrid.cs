namespace exomine.Services.Data
{
    public interface IGrid
    {
        public int Size { get; set; }
        public int Mines{ get; set; }
        public Tile[,] Tiles { get; set; } //X,Y
        public int Width { get; set; }
        public int Height{ get; set; }
        public void Init();
    }
}