using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
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
            _appointmentsService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
            _specialityService = specialityService ?? throw new ArgumentNullException(nameof(specialityService));
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            _providerService = providerService ?? throw new ArgumentNullException(nameof(providerService));
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
                    Log.Information("Successfully retrieved appointments.");
                }
                else
                {
                    // Handle unsuccessful response
                    ViewBag.ErrorMessage = "Failed to load appointments.";
                    Log.Warning("Failed to load appointments: {Response}", appointmentResponse?.Message);
                }

                var specialtyResponse = await _specialityService.GetSpecialityAsync();
                if (specialtyResponse != null && specialtyResponse.IsSuccess)
                {
                    viewModel.Specialties = DeserializeResponse<List<SpecialtyDto>>(specialtyResponse.Result);
                    Log.Information("Successfully retrieved specialties.");
                }

                var locationResponse = await _locationService.GetLocationsAsync();
                if (locationResponse != null && locationResponse.IsSuccess)
                {
                    viewModel.Locations = DeserializeResponse<List<LocationDto>>(locationResponse.Result);
                    Log.Information("Successfully retrieved locations.");
                }

                var providersResponse = await _providerService.GetProviderAsync();
                if (providersResponse != null && providersResponse.IsSuccess)
                {
                    viewModel.Providers = DeserializeResponse<List<HealthcareProviderDto>>(providersResponse.Result);
                    Log.Information("Successfully retrieved providers.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while loading data for appointments schedule.");
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
                Log.Warning("Invalid appointment data received.");
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
                        Log.Information("Successfully booked appointment with ID {AppointmentId}", appointmentId.Value);
                        return RedirectToAction("Schedule", new { appointmentId });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to book the appointment. Please try again.";
                        Log.Warning("Failed to extract appointment ID from response.");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to book the appointment. Please try again.";
                    Log.Warning("Appointment booking failed: {Response}", response?.Message);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while booking the appointment.");
                ViewBag.ErrorMessage = $"An error occurred while booking the appointment: {ex.Message}";
            }

            return RedirectToAction("Schedule");
        }

        // GET: Appointments/Confirmation
        public async Task<IActionResult> Confirmation(int appointmentId)
        {
            if (appointmentId <= 0)
            {
                Log.Warning("Invalid appointment ID {AppointmentId} provided for confirmation.", appointmentId);
                return RedirectToAction("Schedule");
            }

            try
            {
                var appointmentDetails = await _appointmentsService.GetAppointmentAsyncById(appointmentId);

                if (appointmentDetails == null)
                {
                    ViewBag.ErrorMessage = "Appointment not found.";
                    Log.Warning("Appointment with ID {AppointmentId} not found.", appointmentId);
                    return RedirectToAction("Schedule");
                }

                Log.Information("Successfully retrieved appointment details for ID {AppointmentId}", appointmentId);
                return View(appointmentDetails);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while retrieving appointment details for ID {AppointmentId}.", appointmentId);
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
                Log.Error(ex, "An error occurred while deserializing response.");
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
                Log.Error(ex, "An error occurred while extracting appointment ID.");
                ViewBag.ErrorMessage = $"An error occurred while extracting appointment ID: {ex.Message}";
                return null;
            }
        }
    }
}
