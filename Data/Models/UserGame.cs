using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exomine.Data.Models
{
    public class UserGame
    {
        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required]
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public Game? Game { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
        [Required]
        public bool Win { get; set; }

    }
}