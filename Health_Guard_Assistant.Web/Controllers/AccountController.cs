using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using Health_Guard_Assistant.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;


        public AccountController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                LoginRequestDto loginRequestDto = new LoginRequestDto();
                return View(loginRequestDto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while loading the Login view.");
                TempData["error"] = "An error occurred while loading the login page.";
                return RedirectToAction("Error", "Home");
            }
        }
        // POST method for Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                Log.Warning("Invalid login model state for user {Username}.", loginRequestDto.Email);
                return View(loginRequestDto);
            }

            try
            {
                // Call the login service
                ResponseDto responseDto = await _authService.LoginAsync(loginRequestDto);

                // Check if login was successful
                if (responseDto == null || !responseDto.IsSuccess)
                {
                    Log.Warning("Login failed for user {Username}.", loginRequestDto.Email);
                    ModelState.AddModelError("CustomError", responseDto?.Message ?? "Login failed. Please try again.");
                    TempData["error"] = responseDto?.Message ?? "Login failed. Please try again.";
                    return View(loginRequestDto);
                }

                // Safely deserialize login response
                var loginResponseDto = responseDto.Result != null
                    ? JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result))
                    : null;

                if (loginResponseDto == null)
                {
                    Log.Warning("Login response deserialization failed for user {Username}.", loginRequestDto.Email);
                    TempData["error"] = "Login failed due to an internal error. Please try again.";
                    return View(loginRequestDto);
                }

                // Sign in the user and set the token
                await SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);
                TempData["success"] = "Login successful!";
                Log.Information("User {Username} logged in successfully.", loginRequestDto.Email);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred during login for user {Username}.", loginRequestDto.Email);
                TempData["error"] = "An error occurred while processing your login. Please try again.";
                return View(loginRequestDto);
            }
        }

        // Method to handle user sign-in with claims
        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt;

            try
            {
                jwt = handler.ReadJwtToken(model.Token);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Invalid JWT Token", ex);
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            var emailClaim = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
            var subClaim = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var nameClaim = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? emailClaim;

            var roleClaim = jwt.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            if (!string.IsNullOrEmpty(emailClaim))
            {
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, emailClaim));
            }

            if (!string.IsNullOrEmpty(subClaim))
            {
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, subClaim));
            }

            // Use the email as the fallback for the Name claim, like in your first example
            if (!string.IsNullOrEmpty(nameClaim))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, nameClaim));
            }

            if (!string.IsNullOrEmpty(roleClaim))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleClaim));
            }

            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        }
        [HttpGet]
        public IActionResult Register()
        {
            try
            {
                var roleList = new List<SelectListItem>
                {
                    new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                    new SelectListItem{Text=SD.RolePatient,Value=SD.RolePatient},
                    new SelectListItem{Text=SD.RoleDoctor,Value=SD.RoleDoctor},
                };
                ViewBag.RoleList = roleList;
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while loading the Register view.");
                TempData["error"] = "An error occurred while loading the registration page.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            if (!ModelState.IsValid)
            {
                Log.Warning("Invalid registration model state.");
                var roleList = new List<SelectListItem>
                {
                    new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                    new SelectListItem{Text=SD.RolePatient,Value=SD.RolePatient},
                    new SelectListItem{Text=SD.RoleDoctor,Value=SD.RoleDoctor},
                };
                ViewBag.RoleList = roleList;
                return View(registrationRequestDto);
            }

            try
            {
                ResponseDto result = await _authService.RegisterAsync(registrationRequestDto);
                if (result == null || !result.IsSuccess)
                {
                    Log.Warning("Registration failed. Result: {Result}", result);
                    TempData["error"] = result?.Message ?? "Registration failed. Please try again.";
                    return View(registrationRequestDto);
                }

                // Assign role if registration succeeded
                if (string.IsNullOrEmpty(registrationRequestDto.Role))
                {
                    registrationRequestDto.Role = SD.RolePatient;
                }

                ResponseDto assignRole = await _authService.AssignRoleAsync(registrationRequestDto);
                if (assignRole == null || !assignRole.IsSuccess)
                {
                    Log.Warning("Role assignment failed. Result: {AssignRoleResult}", assignRole);
                    TempData["error"] = assignRole?.Message ?? "Role assignment failed.";
                    return View(registrationRequestDto);
                }

                TempData["success"] = "Registration successful!";
                Log.Information("User {Email} registered and role assigned successfully.", registrationRequestDto.Email);
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred during registration.");
                TempData["error"] = "An error occurred while processing your registration.";
                return View(registrationRequestDto);
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                // Add logic for logging out if needed
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _tokenProvider.ClearToken();
                Log.Information("User logged out.");
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred during logout.");
                TempData["error"] = "An error occurred while logging out.";
                return RedirectToAction("Error", "Home");
            }
        }
        // Forgot Password Get Action
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while loading the Forgot Password view.");
                TempData["error"] = "An error occurred while loading the forgot password page.";
                return RedirectToAction("Error", "Home");
            }
        }
        //Forgot password post action
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordDto);
            }
            try
            {
                var response = await _authService.ForgotPassword(forgotPasswordDto);
                if (response == null && !response.IsSuccess)
                {
                    TempData["error"] = response?.Message ?? "An error occurred while sending the reset password email.";
                    return View(forgotPasswordDto);
                }
                TempData["success"] = "Reset password email sent successfully!";
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred during Forgot Password.");
                TempData["error"] = "An error occurred while processing the forgot password request.";
                return View(forgotPasswordDto);
            }
        }
        //reset password get action
        [HttpGet("resetpassword")]
        public IActionResult ResetPassword(string token, string email)
        {
            try
            {
                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
                {
                    return NotFound(); // Token or email is missing
                }

                var model = new ResetPasswordDto { Token = token, Email = email };
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while loading the Reset Password view.");
                TempData["error"] = "An error occurred while loading the reset password page.";
                return RedirectToAction("Error", "Home");
            }

        }

        // Reset Password Post Action
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordDto);
            }

            try
            {
                var response = await _authService.ResetPassword(resetPasswordDto);
                if (response == null || !response.IsSuccess)
                {
                    TempData["error"] = response?.Message ?? "An error occurred while resetting the password.";
                    return View(resetPasswordDto);
                }

                TempData["success"] = "Password reset successfully!";
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred during Reset Password.");
                TempData["error"] = "An error occurred while processing the reset password request.";
                return View(resetPasswordDto);
            }
        }

    }
}
