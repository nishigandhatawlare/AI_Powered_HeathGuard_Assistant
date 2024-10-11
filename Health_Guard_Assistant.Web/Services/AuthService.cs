using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using static Health_Guard_Assistant.Web.Utility.SD;

namespace Health_Guard_Assistant.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto>? AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Post,
                Data = registrationRequestDto,
                Url = AuthApiBase + "/api/auth/assignRole"
            });
        }

        public async Task<ResponseDto>? ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Post,
                Data = forgotPasswordDto,
                Url = AuthApiBase + "/api/auth/forgotpassword"
            }, withBearer: false);
        }

        public async Task<ResponseDto>? LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Post,
                Data = loginRequestDto,
                Url = AuthApiBase + "/api/auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto>? RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Post,
                Data = registrationRequestDto,
                Url = AuthApiBase + "/api/auth/register"
            }, withBearer: false);
        }

        public async Task<ResponseDto>? ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Post,
                Data = resetPasswordDto,
                Url = AuthApiBase + "/api/auth/resetpassword"
            }, withBearer: false);
        }
    }
}
