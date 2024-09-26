using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
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
