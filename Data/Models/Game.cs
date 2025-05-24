using System.ComponentModel.DataAnnotations;
using exomine.Data.Enums;

namespace exomine.Data.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public GridType Type { get; set; }
        [Required]
        public int Difficulty { get; set; }
        [Required]
        [MaxLength(300)]
        public string Bombs { get; set; } = String.Empty;
        [Required]
        [MaxLength(300)]
        public string Revealed { get; set; } = String.Empty;
        [Required]
        [MaxLength(300)]
        public string Known { get; set; } = String.Empty;
        public ICollection<UserGame> PlayedBy { get; set; } = new List<UserGame>();
    }
}