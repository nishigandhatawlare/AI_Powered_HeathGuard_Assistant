using Health_Guard_Assistant.services.AppointmentService.Data;
using Health_Guard_Assistant.services.AppointmentService.Models.Dto;
using Health_Guard_Assistant.services.AppointmentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.services.AppointmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responce;
        public LocationsAPIController(AppDbContext db)
        {
            _db = db;
            _responce = new ResponseDto();
        }
        // GET: api/providers
        [HttpGet]
        public ResponseDto GetLocations()
        {
            try
            {
                IEnumerable<Location> locations = _db.Locations.ToList();
                _responce.Result = locations;
                _responce.IsSuccess = true;
                _responce.Message = "locations retrieved successfully.";
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }

        // GET: api/providers/{id}
        [HttpGet("{id:int}")]
        public ResponseDto GetLocation(int id)
        {
            try
            {
                Location locations = _db.Locations.First(u => u.LocationId == id);
                _responce.Result = locations;
                _responce.IsSuccess = true;
                _responce.Message = "locations retrieved successfully.";
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }

        // POST: api/providers
        [HttpPost]
        public ResponseDto CreateLocation([FromBody] Location locations)
        {
            try
            {
                if (locations == null)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "Invalid locations data.";
                    return _responce;
                }
                //add new providers to the database
                _db.Locations.Add(locations);
                _db.SaveChanges();
                _responce.Result = locations;
                _responce.IsSuccess = true;
                _responce.Message = "locations created successfully.";

            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;

        }

        // PUT: api/providers/{id}
        [HttpPut("{id:int}")]
        public ResponseDto UpdateLocation(int id, [FromBody] Location locations)
        {
            try
            {
                if (locations == null)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "Invalid locations data.";
                    return _responce;
                }
                //retrive existing providers data from Database
                var existingData = _db.Locations.FirstOrDefault(u => u.LocationId == id);
                if (existingData == null)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "locations data not found.";
                    return _responce;
                }
                //update providers 
                existingData.Name = locations.Name;
                

                _db.SaveChanges();
                _responce.Result = existingData;
                _responce.IsSuccess = true;
                _responce.Message = "locations updated successfully.";

            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }

        //DELETE: api/providers/{id

        [HttpDelete("{id:int}")]
        public ResponseDto DeleteProvider(int id)
        {
            try
            {
                //retrive extisting provider
                var existingProvider = _db.Locations.FirstOrDefault(u => u.LocationId == id);
                if (existingProvider == null)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = $"locations not found for id = {id}";
                    return _responce;
                }
                //remove provider from db
                _db.Locations.Remove(existingProvider);
                _db.SaveChanges();

                _responce.IsSuccess = true;
                _responce.Message = $"Data with Id {id} deleted successfully!";

            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }
    }
}
