using Health_Guard_Assistant.services.AuthService.Data;
using Health_Guard_Assistant.services.AuthService.Models;
using Health_Guard_Assistant.services.AuthService.Models.Dto;
using Health_Guard_Assistant.services.AuthService.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Health_Guard_Assistant.services.AuthService.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }


        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            // Create a new ApplicationUser object from the registration data
            ApplicationUser applicationUser = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.FirstName,  // Assuming 'Name' stores first name
                LastName = registrationRequestDto.LastName,  // Last name from the registration form
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                // Validate password and confirm password are the same
                if (registrationRequestDto.Password != registrationRequestDto.ConfirmPassword)
                {
                    throw new Exception("Password and Confirm Password do not match.");
                }

                // Create a user with registration details
                var result = await _userManager.CreateAsync(applicationUser, registrationRequestDto.Password);

                // If user creation is successful, return user details
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.Email == registrationRequestDto.Email);
                    UserDto userDto = new()
                    {
                        UserId = userToReturn.Id,  // No need to parse; Id remains as string
                        FirstName = userToReturn.Name,
                        LastName = userToReturn.LastName,
                        Email = userToReturn.Email,
                        RememberMe = false
                    };
                    return "";
                }
                else
                {
                    return "User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return "Error Occured";
        }

        public Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            throw new NotImplementedException();
        }
    }
}
