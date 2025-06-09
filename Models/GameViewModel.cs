using exomine.Data.Models;

namespace exomine.Models
{
    public class GameViewModel
    {
        public Game Game { get; set; } = new Game();
        public TimeSpan? Time { get; set; }
        public int GameId { get; set; }
    }
}