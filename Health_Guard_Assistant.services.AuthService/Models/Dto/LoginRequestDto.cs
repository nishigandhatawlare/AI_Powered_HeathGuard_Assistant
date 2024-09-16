namespace Health_Guard_Assistant.services.AuthService.Models.Dto
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
