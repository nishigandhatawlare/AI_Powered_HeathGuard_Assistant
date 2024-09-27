using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using Health_Guard_Assistant.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new LoginRequestDto();
            return View(loginRequestDto);
        }
        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>
            {
                new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RolePatient,Value=SD.RolePatient},
                new SelectListItem{Text=SD.RoleDoctor,Value=SD.RoleDoctor},
            };
            //pass roleList to view
            ViewBag.RoleList = roleList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            ResponseDto result = await _authService.RegisterAsync(registrationRequestDto);
            ResponseDto assignRole;
            if (result != null && result.IsSuccess == true)
            {
                if (string.IsNullOrEmpty(registrationRequestDto.Role))
                {
                    registrationRequestDto.Role = SD.RolePatient;
                }
                assignRole = await _authService.AssignRoleAsync(registrationRequestDto);
                if (assignRole != null && assignRole.IsSuccess == true)
                {
                    TempData["success"] = "Registration Successful!";
                    return RedirectToAction(nameof(Login));
                }
            }
            var roleList = new List<SelectListItem>
            {
                new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RolePatient,Value=SD.RolePatient},
                new SelectListItem{Text=SD.RoleDoctor,Value=SD.RoleDoctor},
            };
            //pass roleList to view
            ViewBag.RoleList = roleList;
            return View(registrationRequestDto);
        }
        public IActionResult Logout()
        {
            return View();
        }
        public IActionResult forgotpassword()
        {
            return View();
        }
        // GET: /resetpassword
        [HttpGet("resetpassword")]
        public IActionResult ResetPassword(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                return NotFound(); // Return 404 if token or email is missing
            }
            return NotFound(); // Return 404 if token or email is missing

            // Here you can add logic to validate the token and email
            // And return the view to reset the password
            //return View(new ResetPasswordViewModel { Token = token, Email = email });
        }
    }
}
