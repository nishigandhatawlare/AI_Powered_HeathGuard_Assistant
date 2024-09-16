namespace Health_Guard_Assistant.services.AuthService.Models.Dto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token {  get; set; }
    }
}
