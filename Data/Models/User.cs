using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exomine.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Username { get; set; } = String.Empty;
        [Required]
        public string PasswordHash { get; set; } = String.Empty;
        [Required]
        public string Role { get; set; } = String.Empty;
        [InverseProperty("User")]
        public List<UserGame> GamesPlayed { get; set; } = new List<UserGame>();

        public static User CreateNew(string username, string password)
        {
            return new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = "None"
            };
        }
    }
}