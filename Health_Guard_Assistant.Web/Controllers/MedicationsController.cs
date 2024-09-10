using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class MedicationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
