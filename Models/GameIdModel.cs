using exomine.Data.Models;

namespace exomine.Models
{
    public class GameIdModel
    {
        public int GameId { get; set; }
        public Game Game { get; set; } = new Game();
    }
}