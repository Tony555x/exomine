using System.ComponentModel.DataAnnotations;

namespace exomine.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(UserConstants.UsernameMinLength)]
        [MaxLength(UserConstants.UsernameMaxLength)]
        public string Username { get; set; } = String.Empty;
        [Required]
        public string PasswordHash { get; set; } = String.Empty;
        [Required]
        public string Role { get; set; } = String.Empty;
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