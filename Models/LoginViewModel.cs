namespace exomine.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string? ErrorMessage { get; set; }
    }
}