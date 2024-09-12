using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentsService;
        private readonly ISpecialityService _specialityService;
        private readonly ILocationService _locationService;
        private readonly IProviderService _providerService;

        public AppointmentsController(IAppointmentService appointmentService, ISpecialityService specialityService,
            ILocationService locationService, IProviderService providerService)
        {
            _appointmentsService = appointmentService;
            _specialityService = specialityService;
            _locationService = locationService;
            _providerService = providerService;
        }

        // GET: Appointments/Schedule
        public async Task<IActionResult> Schedule()
        {
            var viewModel = new AppointmentViewModel();

            try
            {
                var appointmentResponse = await _appointmentsService.GetAppointmentsAsync();
                if (appointmentResponse != null && appointmentResponse.IsSuccess)
                {
                    viewModel.Appointments = DeserializeResponse<List<AppointmentDto>>(appointmentResponse.Result);
                }
                else
                {
                    // Handle unsuccessful response
                    ViewBag.ErrorMessage = "Failed to load appointments.";
                }

                var specialtyResponse = await _specialityService.GetSpecialityAsync();
                if (specialtyResponse != null && specialtyResponse.IsSuccess)
                {
                    viewModel.Specialties = DeserializeResponse<List<SpecialtyDto>>(specialtyResponse.Result);
                }

                var locationResponse = await _locationService.GetLocationsAsync();
                if (locationResponse != null && locationResponse.IsSuccess)
                {
                    viewModel.Locations = DeserializeResponse<List<LocationDto>>(locationResponse.Result);
                }

                var providersResponse = await _providerService.GetProviderAsync();
                if (providersResponse != null && providersResponse.IsSuccess)
                {
                    viewModel.Providers = DeserializeResponse<List<HealthcareProviderDto>>(providersResponse.Result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                ViewBag.ErrorMessage = $"An error occurred while loading data: {ex.Message}";
            }

            return View(viewModel);
        }

        // POST: Appointments/Book
        [HttpPost]
        public async Task<IActionResult> Book(AppointmentDto appointmentDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Invalid data. Please review the form.";
                return RedirectToAction("Schedule");
            }

            try
            {
                var response = await _appointmentsService.CreateAppointmentAsync(appointmentDto);

                if (response != null && response.IsSuccess)
                {
                    var appointmentId = ExtractAppointmentId(response.Result);

                    if (appointmentId.HasValue)
                    {
                        return RedirectToAction("Schedule", new { appointmentId });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to book the appointment. Please try again.";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to book the appointment. Please try again.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                ViewBag.ErrorMessage = $"An error occurred while booking the appointment: {ex.Message}";
            }

            return RedirectToAction("Schedule");
        }

        // GET: Appointments/Confirmation
        public async Task<IActionResult> Confirmation(int appointmentId)
        {
            if (appointmentId <= 0)
            {
                return RedirectToAction("Schedule");
            }

            try
            {
                var appointmentDetails = await _appointmentsService.GetAppointmentAsyncById(appointmentId);

                if (appointmentDetails == null)
                {
                    ViewBag.ErrorMessage = "Appointment not found.";
                    return RedirectToAction("Schedule");
                }

                return View(appointmentDetails);
            }
            catch (Exception ex)
            {
                // Log the exception
                ViewBag.ErrorMessage = $"An error occurred while retrieving appointment details: {ex.Message}";
                return RedirectToAction("Schedule");
            }
        }

        // Helper method to deserialize response
        private T DeserializeResponse<T>(object result)
        {
            if (result == null)
                return default;

            try
            {
                return JsonConvert.DeserializeObject<T>(Convert.ToString(result));
            }
            catch (JsonSerializationException ex)
            {
                // Log the exception
                ViewBag.ErrorMessage = $"An error occurred while deserializing response: {ex.Message}";
                return default;
            }
        }

        // Helper method to extract appointmentId
        private int? ExtractAppointmentId(object result)
        {
            if (result == null)
                return null;

            try
            {
                var response = JsonConvert.DeserializeObject<dynamic>(Convert.ToString(result));
                return response?.appointmentId != null ? (int?)response.appointmentId : null;
            }
            catch (JsonSerializationException ex)
            {
                // Log the exception
                ViewBag.ErrorMessage = $"An error occurred while extracting appointment ID: {ex.Message}";
                return null;
            }
        }
    }
}
