using System.ComponentModel.DataAnnotations;
using exomine.Data.Enums;

namespace exomine.Models
{
    public class NewGameViewModel
    {
        [Required]
        [Range(3, 20)]
        public int Size { get; set; }

        [Range(0, 10000)]
        public int Difficulty { get; set; } //not required

        [Required]
        public GridType Type { get; set; }

        [Required]
        public bool UseExisting{ get; set; }
    }
}