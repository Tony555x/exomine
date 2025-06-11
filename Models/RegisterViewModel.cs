using System.ComponentModel.DataAnnotations;

namespace exomine.Models
{
    public class RegisterViewModel
    {
        public string? Username { get; set; } = String.Empty;
        public string? Password { get; set; } = String.Empty;
        public string? ConfirmPassword { get; set; } = String.Empty;
        public string UsernameErrorMessage { get; set; } = String.Empty;
        public string PasswordErrorMessage { get; set; } = String.Empty;
        public string ConfirmErrorMessage { get; set; } = String.Empty;
    }
}