using System.ComponentModel.DataAnnotations;

namespace exomine.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50), MinLength(3)]
        public string Username { get; set; } = String.Empty;
    }
}