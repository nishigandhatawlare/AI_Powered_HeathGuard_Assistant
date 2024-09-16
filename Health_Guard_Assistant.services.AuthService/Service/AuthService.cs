using Health_Guard_Assistant.services.AuthService.Models.Dto;
using Health_Guard_Assistant.services.AuthService.Service.IService;

namespace Health_Guard_Assistant.services.AuthService.Service
{
    public class AuthService : IAuthService
    {
        public Task<bool> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            throw new NotImplementedException();
        }
    }
}
