using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult AdminDashboard()
        {
            ViewData["ActivePage"] = "Dashboard";
            return View();
        }
        public IActionResult PatientDashboard()
        {
            ViewData["ActivePage"] = "Dashboard";
            return View();
        }
        public IActionResult DoctorDashboard()
        {
            ViewData["ActivePage"] = "Dashboard";
            return View();
        }
    }
}
