using Health_Guard_Assistant.services.AppointmentService.Data;
using Health_Guard_Assistant.services.AppointmentService.Models.Dto;
using Health_Guard_Assistant.services.AppointmentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Health_Guard_Assistant.services.AppointmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class SpecialtiesAPIController : ControllerBase
    {
        
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public SpecialtiesAPIController(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }
        // GET: api/appointments
        [HttpGet]
        public ResponseDto GetSpecialties()
        {
            try
            {
                IEnumerable<Specialty> specialties= _db.Specialties.ToList();
                _response.Result = _mapper.Map<IEnumerable<SpecialtyDto>>(specialties);
                _response.IsSuccess = true;
                _response.Message = "specialties Data retrieved successfully!";
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
        public ResponseDto GetSpecialty(int id)
        {
            try
            {
                Specialty specialty = _db.Specialties.First(u => u.SpecialtyId == id);
                _response.Result = _mapper.Map<SpecialtyDto>(specialty);
                _response.IsSuccess = true;
                _response.Message = $"specialty Data retrieved successfully with id = {id}!";
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
        public ResponseDto CreateSpecialty([FromBody] SpecialtyDto specialtyDto)
        {
            try
            {
                if (specialtyDto == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid provider data.";
                    return _response;
                }
                //before adding map first
                Specialty specialty = _mapper.Map<Specialty>(specialtyDto);
                _db.Specialties.Add(specialty);
                _db.SaveChanges();
                _response.Result = specialty;
                _response.IsSuccess = true;
                _response.Message = "specialty created successfully.";

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
        public ResponseDto UpdateSpecialty(int id, [FromBody] SpecialtyDto specialtyDto)
        {
            try
            {
                if (specialtyDto == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid specialty data.";
                    return _response;
                }
                //retrive existing providers data from Database
                var existingData = _db.Specialties.FirstOrDefault(u => u.SpecialtyId == id);
                if (existingData == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "specialty data not found.";
                    return _response;
                }
                //before update map first
                _mapper.Map(specialtyDto, existingData);
                //existingData.Name = specialty.Name;
                _db.SaveChanges();
                _response.Result = specialtyDto;
                _response.IsSuccess = true;
                _response.Message = "specialty updated successfully.";

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
        public ResponseDto DeleteSpecialty(int id)
        {
            try
            {
                //retrive extisting provider
                var existingappointment = _db.Specialties.FirstOrDefault(u => u.SpecialtyId == id);
                if (existingappointment == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Appointment not found for id = {id}";
                    return _response;
                }
                //remove provider from db
                _db.Specialties.Remove(existingappointment);
                _db.SaveChanges();

                _response.IsSuccess = true;
                _response.Message = $"Data with SpecialtyId {id} deleted successfully!";

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
