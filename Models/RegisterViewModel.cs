using System.ComponentModel.DataAnnotations;

namespace exomine.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(UserConstants.UsernameMinLength)]
        [MaxLength(UserConstants.UsernameMaxLength)]
        public string Username { get; set; } = String.Empty;
        [Required]
        [MinLength(UserConstants.PasswordMinLength)]
        [MaxLength(UserConstants.PasswordMaxLength)]
        public string Password { get; set; } = String.Empty;
        public string ConfirmPassword { get; set; } = String.Empty;
        public string? ErrorMessage { get; set; }
    }
}