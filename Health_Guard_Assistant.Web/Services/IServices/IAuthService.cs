using Health_Guard_Assistant.Web.Models;

namespace Health_Guard_Assistant.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto>? LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto>? RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto>? ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        Task<ResponseDto>? ResetPassword(ResetPasswordDto resetPasswordDto);
        Task<ResponseDto>? AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}
