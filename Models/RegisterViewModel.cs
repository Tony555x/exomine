namespace exomine.Models
{
    public class RegisterViewModel
    {
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string ConfirmPassword { get; set; } = String.Empty;
        public string? ErrorMessage { get; set; }
    }
}