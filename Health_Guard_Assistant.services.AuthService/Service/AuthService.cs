using Health_Guard_Assistant.services.AuthService.Data;
using Health_Guard_Assistant.services.AuthService.Models;
using Health_Guard_Assistant.services.AuthService.Models.Dto;
using Health_Guard_Assistant.services.AuthService.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Health_Guard_Assistant.services.AuthService.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IEmailService _emailService; // Add the email service here

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailService = emailService;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            //if user is not null 
            if (user != null)
            {
                //if role is not present
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exists
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }
        public async Task<bool> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            // Retrieve the user based on the email provided
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

            // Check if the user exists
            if (user == null)
            {
                // Return false if the user does not exist to avoid exposing information
                return false;
            }

            // Generate a password reset token
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Create the reset link (assumed you have a client-side URL to reset the password)
            var resetLink = $"https://localhost:7252/resetpassword?token={Uri.EscapeDataString(resetToken)}&email={Uri.EscapeDataString(user.Email)}";

            // Send the reset link via email (implement this method in your email service)
            var emailSent = await _emailService.SendPasswordResetEmail(user.Email, resetLink);

            // Return whether the email was successfully sent
            return emailSent;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            // Retrieve the existing user with the requested username
            var user = await _db.ApplicationUsers
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.Email.ToLower());

            // Check if the user was found
            if (user == null)
            {
                return new LoginResponseDto
                {
                    User = null,
                    Token = "" // User not found
                };
            }

            // Check if the password is valid
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!isValid)
            {
                return new LoginResponseDto
                {
                    User = null,
                    Token = "" // Invalid password
                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            // If user was found and password is valid, generate JWT Token
            var token = _jwtTokenGenerator.GenerateToken(user,roles);

            // Create a UserDto with necessary user information
            UserDto userDto = new()
            {
                UserId = user.Id,  // No need to parse; Id remains as string
                FirstName = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                RememberMe = false
            };

            // Create and return LoginResponseDto
            return new LoginResponseDto
            {
                User = userDto,
                Token = token // Assign the generated token here
            };
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

        public async Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            // Retrieve the user by email
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return false; // User not found
            }

            // Validate new password and confirmation
            if (resetPasswordDto.Password != resetPasswordDto.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match.");
            }

            // Implement your password policy checks here
            if (!IsValidPassword(resetPasswordDto.Password))
            {
                throw new ArgumentException("Password does not meet security standards.");
            }

            // Reset the password using the token provided
            var resetResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            return resetResult.Succeeded;
        }

        private bool IsValidPassword(string password)
        {
            // Add your password validation logic (length, complexity, etc.)
            if (password.Length < 8) return false; // Example: Minimum length check
            // Add more checks as necessary
            return true;
        }

    }
}
