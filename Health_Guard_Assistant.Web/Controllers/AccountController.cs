﻿using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using Health_Guard_Assistant.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using System;

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

        public IActionResult Logout()
        {
            try
            {
                // Add logic for logging out if needed
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
