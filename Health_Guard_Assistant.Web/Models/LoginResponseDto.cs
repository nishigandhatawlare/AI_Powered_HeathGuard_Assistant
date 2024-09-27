namespace Health_Guard_Assistant.Web.Models
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }       // JWT or authentication token
    }
}
