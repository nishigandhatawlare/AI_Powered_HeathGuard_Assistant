using AutoMapper;
using Azure;
using Health_Guard_Assistant.services.AppointmentService.Data;
using Health_Guard_Assistant.services.AppointmentService.Models;
using Health_Guard_Assistant.services.AppointmentService.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.services.AppointmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public AppointmentAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }
        // GET: api/appointments
        [HttpGet]
        public ResponseDto GetAppointments()
        {
            try
            {
                IEnumerable<Appointment> appointments = _db.Appointments.ToList();
                _response.Result = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
                _response.IsSuccess = true;
                _response.Message = "Appointment Data retrieved successfully!";
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        // GET: api/appointments/{id}
        [HttpGet("{id:int}")]
        public ResponseDto GetAppointment(int id)
        {
            try
            {
                Appointment appointments = _db.Appointments.First(u => u.AppointmentId == id);
                _response.Result = _mapper.Map<AppointmentDto>(appointments);
                _response.IsSuccess = true;
                _response.Message = $"Appointment Data retrieved successfully with id = {id}!";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // POST: api/appointments
        [HttpPost]
        public ResponseDto CreateAppointment([FromBody] AppointmentDto appointmentdto)
        {
            try
            {
                if (appointmentdto == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid provider data.";
                    return _response;
                }
                //map appointmentdto to appointments then add to db
                Appointment appontments = _mapper.Map<Appointment>(appointmentdto);
                //add new providers to the database
                _db.Appointments.Add(appontments);
                _db.SaveChanges();
                _response.Result = appointmentdto;
                _response.IsSuccess = true;
                _response.Message = "Appointments created successfully.";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // PUT: api/appointments/{id}
        [HttpPut("{id:int}")]
        public ResponseDto UpdateAppointment(int id, [FromBody] AppointmentDto appointmentdto)
        {
            try
            {
                if (appointmentdto == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid Appointment data.";
                    return _response;
                }
                //retrive existing providers data from Database
                var existingData = _db.Appointments.FirstOrDefault(u => u.AppointmentId == id);
                if (existingData == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Appointment data not found.";
                    return _response;
                }

                //mapped before update 
                _mapper.Map(appointmentdto, existingData);
                _db.SaveChanges();
                _response.Result = appointmentdto;
                _response.IsSuccess = true;
                _response.Message = "Appointment updated successfully.";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // DELETE: api/appointments/{id}
        [HttpDelete("{id}")]
        public ResponseDto DeleteAppointment(int id)
        {
            try
            {
                //retrive extisting provider
                var existingappointment = _db.Appointments.FirstOrDefault(u => u.AppointmentId == id);
                if (existingappointment == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Appointment not found for id = {id}";
                    return _response;
                }
                //remove provider from db
                _db.Appointments.Remove(existingappointment);
                _db.SaveChanges();

                _response.IsSuccess = true;
                _response.Message = $"Data with Id {id} deleted successfully!";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
