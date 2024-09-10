using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class HealthRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
