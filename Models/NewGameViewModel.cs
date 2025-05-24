using System.ComponentModel.DataAnnotations;
using exomine.Data.Enums;

namespace exomine.Models
{
    public class NewGameViewModel
    {
        [Required]
        [Range(3, 10)]
        public int Size { get; set; }

        [Required]
        public int Difficulty { get; set; }

        [Required]
        public GridType Type { get; set; }
    }
}