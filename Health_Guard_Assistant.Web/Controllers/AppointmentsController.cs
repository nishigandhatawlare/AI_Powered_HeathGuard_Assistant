using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentsService;
        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentsService = appointmentService;
        }
        public async Task<IActionResult> Schedule()
        {
            List<AppointmentDto>? list = new();
            ResponseDto response = await _appointmentsService.GetAppointmentsAsync();
            if (response != null && response.IsSuccess == true)
            {
                list = JsonConvert.DeserializeObject<List<AppointmentDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
