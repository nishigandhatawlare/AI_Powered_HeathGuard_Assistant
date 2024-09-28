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

namespace Health_Guard_Assistant.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
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
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequestDto);
            }

            try
            {
                // Call the login service
                var responseDto = await _authService.LoginAsync(loginRequestDto);

                if (responseDto == null || !responseDto.IsSuccess)
                {
                    ModelState.AddModelError("CustomError", responseDto?.Message ?? "Login failed. Please try again.");
                    TempData["error"] = responseDto?.Message ?? "Login failed. Please try again.";
                    return View(loginRequestDto);
                }

                var loginResponseDto = responseDto.Result != null
                    ? JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result))
                    : null;

                if (loginResponseDto == null)
                {
                    TempData["error"] = "Login failed due to an internal error. Please try again.";
                    return View(loginRequestDto);
                }

                // Handle Authentication
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginResponseDto.User.Email),
            new Claim(ClaimTypes.NameIdentifier, loginResponseDto.User.UserId.ToString())
            // Add more claims if necessary
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Remember Me functionality - set cookie expiration based on checkbox value
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = loginRequestDto.RememberMe,
                    ExpiresUtc = loginRequestDto.RememberMe
                        ? DateTimeOffset.UtcNow.AddDays(30) // 30 days if "Remember Me" checked
                        : DateTimeOffset.UtcNow.AddHours(1) // 1 hour session if not checked
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                // Redirect after successful login
                TempData["success"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred during login.";
                return View(loginRequestDto);
            }
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

        //[HttpGet("resetpassword")]
        //public IActionResult ResetPassword(string token, string email)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
        //        {
        //            Log.Warning("Invalid reset password request. Token or email is missing.");
        //            return NotFound(); // Return 404 if token or email is missing
        //        }

        //        Log.Information("Reset password request for email {Email} with token {Token}.", email, token);
        //        // You can add logic to validate the token and email
        //        // return View(new ResetPasswordViewModel { Token = token, Email = email });

        //        return View(new ResetPasswordViewModel { Token = token, Email = email });
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Error occurred during ResetPassword.");
        //        TempData["error"] = "An error occurred while processing the reset password request.";
        //        return RedirectToAction("Error", "Home");
        //    }
        //}
    }
}
