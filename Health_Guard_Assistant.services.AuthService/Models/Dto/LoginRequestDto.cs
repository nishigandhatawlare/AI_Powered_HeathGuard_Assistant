namespace Health_Guard_Assistant.services.AuthService.Models.Dto
{
    public class LoginRequestDto
    {
        public string Email { get; set; }       // Maps to "Email Address" input
        public string Password { get; set; }    // Maps to "Password" input
        public bool RememberMe { get; set; }    // Maps to "Remember Me" checkbox
    }
}
