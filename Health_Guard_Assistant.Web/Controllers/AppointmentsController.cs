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
					TempData["success"] = "Appointments loaded successfully.";
					Log.Information("Successfully retrieved appointments.");
				}
				else
				{
					TempData["error"] = appointmentResponse?.Message ?? "Failed to load appointments.";
					ViewBag.ErrorMessage = "Failed to load appointments.";
					Log.Warning("Failed to load appointments: {Response}", appointmentResponse?.Message);
				}

				var specialtyResponse = await _specialityService.GetSpecialityAsync();
				if (specialtyResponse != null && specialtyResponse.IsSuccess)
				{
					viewModel.Specialties = DeserializeResponse<List<SpecialtyDto>>(specialtyResponse.Result);
					TempData["success"] = "Specialties loaded successfully.";
					Log.Information("Successfully retrieved specialties.");
				}

				var locationResponse = await _locationService.GetLocationsAsync();
				if (locationResponse != null && locationResponse.IsSuccess)
				{
					viewModel.Locations = DeserializeResponse<List<LocationDto>>(locationResponse.Result);
					TempData["success"] = "Locations loaded successfully.";
					Log.Information("Successfully retrieved locations.");
				}

				var providersResponse = await _providerService.GetProviderAsync();
				if (providersResponse != null && providersResponse.IsSuccess)
				{
					viewModel.Providers = DeserializeResponse<List<HealthcareProviderDto>>(providersResponse.Result);
					TempData["success"] = "Providers loaded successfully.";
					Log.Information("Successfully retrieved providers.");
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while loading data for appointments schedule.");
				TempData["error"] = "An error occurred while loading data: " + ex.Message;
			}

			ViewData["ActivePage"] = "Appointments";
			return View(viewModel);
		}


		// POST: Appointments/Book
		[HttpPost]
        public async Task<IActionResult> Book(AppointmentDto appointmentDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid data. Please review the form.";
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
                        TempData["success"] = "Successfully booked appointment with ID " + appointmentId.Value;
                        Log.Information("Successfully booked appointment with ID {AppointmentId}", appointmentId.Value);
                        return RedirectToAction("Schedule", new { appointmentId });
                    }
                    else
                    {
                        TempData["error"] = "Failed to book the appointment. Please try again.";
                        Log.Warning("Failed to extract appointment ID from response.");
                    }
                }
                else
                {
                    TempData["error"] = response?.Message ?? "Failed to book the appointment. Please try again.";
                    Log.Warning("Appointment booking failed: {Response}", response?.Message);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while booking the appointment.");
                TempData["error"] = "An error occurred while booking the appointment: " + ex.Message;
            }

            return RedirectToAction("Schedule");
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
        private string? ExtractProviderName(object result)
        {
            if (result == null)
                return null;

            try
            {
                // Convert the result to string and log the raw result
                var resultString = Convert.ToString(result);
                Log.Information("Provider API result: " + resultString); // Log the raw result

                // Check if resultString is valid JSON (starts with '{' or '[')
                if (!string.IsNullOrWhiteSpace(resultString) &&
                    (resultString.Trim().StartsWith("{") || resultString.Trim().StartsWith("[")))
                {
                    // Deserialize the result string into a dynamic object
                    var response = JsonConvert.DeserializeObject<dynamic>(resultString);

                    // Check if response contains ProviderName
                    return response?.ProviderName != null ? (string?)response.ProviderName : null;
                }
                else
                {
                    // Log a warning and inspect the actual result string content
                    Log.Warning($"Invalid JSON format in result. Actual content: {resultString ?? "null"}");

                    // Optionally, output more details for debugging purposes
                    Log.Warning($"Result type: {result?.GetType().FullName}");

                    return null;
                }

            }
            catch (JsonReaderException ex)
            {
                // Log the specific JsonReaderException error and return null
                Log.Error(ex, "An error occurred while extracting provider name due to invalid JSON.");
                ViewBag.ErrorMessage = $"An error occurred while extracting provider name: {ex.Message}";
                return null;
            }
            catch (Exception ex)
            {
                // Handle any other exceptions and log them
                Log.Error(ex, "An unexpected error occurred.");
                return null;
            }
        }



        // POST: Appointments/Update
        [HttpPost]
        public async Task<IActionResult> Update(AppointmentDto appointmentDto)
        {
            ModelState.Remove("ProviderName");

            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid data. Please review the form.";
                return RedirectToAction("Schedule");
            }

            try
            {
                var response = await _appointmentsService.UpdateAppointmentAsync(appointmentDto.AppointmentId, appointmentDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Appointment updated successfully.";
                    return RedirectToAction("Schedule");
                }
                else
                {
                    TempData["error"] = "Failed to update the appointment.";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating appointment.");
                TempData["error"] = "An error occurred while updating appointment.";
            }

            return RedirectToAction("Schedule");
        }

        // POST: Appointments/Cancel
        [HttpPost]
        public async Task<IActionResult> Cancel(int appointmentId)
        {
            try
            {
                var response = await _appointmentsService.DeleteAppointmentAsync(appointmentId);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Appointment canceled successfully.";
                    return RedirectToAction("Schedule");
                }
                else
                {
                    TempData["error"] = "Failed to cancel the appointment.";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while canceling appointment.");
                TempData["error"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction("Schedule");
        }

    }
}
