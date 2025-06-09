using System.ComponentModel.DataAnnotations;
using exomine.Data.Enums;

namespace exomine.Data.Models
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  // e.g., "Win a hexagon game at size 5"
        public GridType? GridType { get; set; } // e.g., "Hexagon", "Square", "Triangle", "SquareTriHex"
        public int? MinSize { get; set; }
        public int? MinDifficulty { get; set; }
        public int? MaxTimeSeconds { get; set; }
    }
}