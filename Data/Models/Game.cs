using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [MaxLength(1000)]
        public string Bombs { get; set; } = String.Empty;
        [Required]
        [MaxLength(1000)]
        public string Revealed { get; set; } = String.Empty;
        [Required]
        [MaxLength(1000)]
        public string Known { get; set; } = String.Empty;
        [InverseProperty("Game")]
        public ICollection<UserGame> PlayedBy { get; set; } = new List<UserGame>();
    }
}