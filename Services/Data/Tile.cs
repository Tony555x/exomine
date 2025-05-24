namespace exomine.Services.Data
{
    public class Tile(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public bool Bomb { get; set; }//is bomb?
        public int Bombs { get; set; }//adjacent bombs
        public int Current { get; set; } = 0;//currently visible adjacent bombs
        public bool Revealed { get; set; } = false;//is tile permanently revealed
        public bool Known { get; set; } = false;//is adjacent bomb amount visible
        public bool Lock { get; set; } = false;//is tile used in dfs
        public bool TempValue { get; set; }//value assumption

        public List<Tile> Adj { get; set; } = new List<Tile>();
        
    }
}