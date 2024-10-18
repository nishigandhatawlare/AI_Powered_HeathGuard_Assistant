using AutoMapper;
using Azure;
using Health_Guard_Assistant.services.MedicationService.Data;
using Health_Guard_Assistant.services.MedicationService.Models;
using Health_Guard_Assistant.services.MedicationService.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.services.MedicationService.Controllers
{
    [Route("api/medication")]
    [ApiController]
	[Authorize]
	public class MedicationAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public MedicationAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }
        // GET: api/medication (Admin and Doctor can view all medications)
        [HttpGet]
        [Authorize(Roles = "ADMIN,DOCTOR")]
        public ResponseDto GetMedications()
        {
            try
            {
                IEnumerable<Medication> medications = _db.Medications.ToList();
                _response.Result = _mapper.Map<IEnumerable<MedicationDto>>(medications);
                _response.IsSuccess = true;
                _response.Message = "Medication Data retrieved successfully!";
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        // GET: api/medication/{id} (Admin, Doctor, and Patient can view an medication)
        //(Patients should only access their own medication)
        [HttpGet("{id:int}")]
        [Authorize(Roles = "ADMIN,DOCTOR,PATIENT")]
        public ResponseDto GetMedication(int id)
        {
            try
            {
                Medication medications = _db.Medications.First(u => u.Id == id);
                _response.Result = _mapper.Map<MedicationDto>(medications);
                _response.IsSuccess = true;
                _response.Message = $"medication Data retrieved successfully with id = {id}!";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // POST: api/medication (Admin and Doctor can create medication)
        [HttpPost]
        [Authorize(Roles = "ADMIN,DOCTOR")]
        public ResponseDto CreateMedication([FromBody] MedicationDto medicationDto)
        {
            try
            {
                if (medicationDto == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid medication data.";
                    return _response;
                }

                // Map medicationdto to the medication entity and add to db
                Medication medication = _mapper.Map<Medication>(medicationDto);

                // Add the new appointment to the database
                _db.Medications.Add(medication);
                _db.SaveChanges(); // After this, the appointment.Id will be populated

                // Return the appointment DTO with the generated ID
                medicationDto.Id = medication.Id; // Assuming appointmentdto has an Id property

                _response.Result = medicationDto;
                _response.IsSuccess = true;
                _response.Message = "medication created successfully.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // PUT: api/medication/{id} (Admin and Doctor can update medication)
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,DOCTOR")]
        public ResponseDto UpdateMedication(int id, [FromBody] MedicationDto medicationDto)
        {
            try
            {
                if (medicationDto == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid medication data.";
                    return _response;
                }
                //retrive existing providers data from Database
                var existingData = _db.Medications.FirstOrDefault(u => u.Id == id);
                if (existingData == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "medication data not found.";
                    return _response;
                }

                //mapped before update 
                _mapper.Map(medicationDto, existingData);
                _db.SaveChanges();
                _response.Result = medicationDto;
                _response.IsSuccess = true;
                _response.Message = "medication updated successfully.";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // DELETE: api/medication/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN,DOCTOR")]   //admin and doctor can delete medication 
        public ResponseDto DeleteMedication(int id)
        {
            try
            {
                //retrive extisting provider
                var existingmedication = _db.Medications.FirstOrDefault(u => u.Id == id);
                if (existingmedication == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"medication not found for id = {id}";
                    return _response;
                }
                //remove provider from db
                _db.Medications.Remove(existingmedication);
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

        // GET: api/medications/ByUser/{id} (Patients can view their own medications)
        [HttpGet("ByUser/{id}")]
        [Authorize(Roles = "ADMIN,DOCTOR,PATIENT")]
        public IActionResult GetMedicationByUserId(string id)
        {
            try
            {
                var medications = _db.Medications.Where(a => a.UserId == id).ToList();

                if (!medications.Any())
                {
                    _response.IsSuccess = false;
                    _response.Message = $"No medications found for user with ID {id}.";
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<IEnumerable<MedicationDto>>(medications);
                _response.IsSuccess = true;
                _response.Message = $"medications for user ID {id} retrieved successfully.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }
    }
}
