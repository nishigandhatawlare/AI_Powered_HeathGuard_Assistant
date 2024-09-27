using Health_Guard_Assistant.services.AuthService.Models.Dto;

namespace Health_Guard_Assistant.services.AuthService.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        // Method for password reset request
        Task<bool> ForgotPassword(ForgotPasswordDto forgotPasswordDto);

        // Method for resetting the password
        Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto);
        Task<bool> AssignRole(string email, string roleName);

    }
}
