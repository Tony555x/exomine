namespace exomine.Models
{
    public class LoginViewModel
    {
        public string? Username { get; set; } = String.Empty;
        public string? Password { get; set; } = String.Empty;
        public string UsernameErrorMessage { get; set; } = String.Empty;
        public string PasswordErrorMessage { get; set; } = String.Empty;
    }
}