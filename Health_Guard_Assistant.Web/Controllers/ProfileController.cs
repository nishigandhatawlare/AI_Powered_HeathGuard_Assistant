using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "Profile";
            return View();
        }
    }
}
