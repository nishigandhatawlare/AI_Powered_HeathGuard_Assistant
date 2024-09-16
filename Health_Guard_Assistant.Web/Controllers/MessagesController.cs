using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "Messages";
            return View();
        }
    }
}
