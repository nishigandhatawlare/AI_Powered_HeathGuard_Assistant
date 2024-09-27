using Health_Guard_Assistant.services.AuthService.Models.Dto;
using Health_Guard_Assistant.services.AuthService.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.services.AuthService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.Message = errorMessage;
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (loginRequestDto == null || string.IsNullOrEmpty(loginRequestDto.Email) || string.IsNullOrEmpty(loginRequestDto.Password))
            {
                _response.IsSuccess = false;
                _response.Message = "Email and Password are required.";
                return BadRequest(_response);
            }

            var loginResponse = await _authService.Login(loginRequestDto);

            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "UserName or Password is Incorrect!";
                return BadRequest(_response);
            }

            _response.IsSuccess = true;
            _response.Result = loginResponse; // Send back the token and user information in the response
            return Ok(_response);
        }


        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (forgotPasswordDto == null || string.IsNullOrEmpty(forgotPasswordDto.Email))
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid request data";
                return BadRequest(_response);
            }

            // Call the ForgotPassword method in the AuthService
            var result = await _authService.ForgotPassword(forgotPasswordDto);

            if (!result)
            {
                _response.IsSuccess = false;
                _response.Message = "If an account with the provided email exists, a password reset link has been sent.";
                return Ok(_response); // Always return Ok to prevent email enumeration
            }

            _response.IsSuccess = true;
            _response.Message = "Password reset link has been sent.";
            return Ok(_response);
        }
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto == null || string.IsNullOrEmpty(resetPasswordDto.Email) ||
                string.IsNullOrEmpty(resetPasswordDto.Password) ||
                string.IsNullOrEmpty(resetPasswordDto.ConfirmPassword) ||
                string.IsNullOrEmpty(resetPasswordDto.Token))
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid request data";
                return BadRequest(_response);
            }

            // Call the ResetPassword method in the AuthService
            var result = await _authService.ResetPassword(resetPasswordDto);
            if (!result)
            {
                _response.IsSuccess = false;
                _response.Message = "Password reset failed. Please verify the token and try again.";
                return BadRequest(_response);
            }

            _response.IsSuccess = true;
            _response.Message = "Password has been reset successfully.";
            return Ok(_response);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessfull = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessfull)
            {
                _response.IsSuccess = false;
                _response.Message = "Error Encountered!";
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}
