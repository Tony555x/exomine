namespace exomine.Services.Data
{
    public class Tile(int x, int y)
    {

        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public bool Bomb { get; set; }//is bomb?
        public int Bombs { get; set; }//total adjacent bombs
        public int CurrentBombs { get; set; } = 0;//currently visible adjacent bombs
        public int RemainingBombs { get { return Bombs - CurrentBombs; } }//remaining current adjacent bombs
        public int AdjCount { get { return Adj.Count; } }
        public int Empty { get; set; } = 0;//currently unrevealed adjacent tiles
        public bool Revealable { get; set; } = false;//is tile revealaled, or revealable by solver
        public bool Revealed { get; set; } = false;//is tile permanently revealed
        public bool Known { get; set; } = false;//is adjacent bomb amount visible
        public bool Lock { get; set; } = false;//is tile used in bfs or backtrack
        public bool TempBomb { get; set; }//value assumption

        public List<Tile> Adj { get; set; } = new List<Tile>();
        public bool SetBomb(bool val)
        {
            TempBomb = val;
            bool ok = true;
            for (int j = 0; j < Adj.Count; j++)
            {
                Tile t2 = Adj[j];
                if (!t2.Known || !t2.Revealable || t2.Bomb) continue;
                t2.Empty--;
                if (val) t2.CurrentBombs++;
                if (t2.RemainingBombs < t2.Empty) ok = false;
                if (t2.RemainingBombs < 0) ok = false;
            }
            return ok;
        }
        public void Clear()
        {
            for (int j = 0; j < Adj.Count; j++)
            {
                Tile t2 = Adj[j];
                if (!t2.Known || !t2.Revealable || t2.Bomb) continue;
                t2.Empty--;
                if (TempBomb) t2.CurrentBombs++;
            }
            TempBomb = false;
        }
        public void Reveal(bool perm)
        {
            Revealable = true;
            if (perm) Revealed = true;
            if (Bomb) Known = true;
            for (int j = 0; j < Adj.Count; j++)
            {
                Tile t2 = Adj[j];
                if (t2.Bomb) continue;
                t2.Empty--;
                if (Bomb) t2.CurrentBombs++;
            }
        }
    }
}