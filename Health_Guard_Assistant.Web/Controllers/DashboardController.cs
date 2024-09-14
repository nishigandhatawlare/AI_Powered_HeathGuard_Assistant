using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "Dashboard";
            return View();
        }
    }
}
