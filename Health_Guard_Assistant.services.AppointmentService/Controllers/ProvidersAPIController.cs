using AutoMapper;
using Health_Guard_Assistant.services.AppointmentService.Data;
using Health_Guard_Assistant.services.AppointmentService.Models;
using Health_Guard_Assistant.services.AppointmentService.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Health_Guard_Assistant.services.AppointmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responce;
        private readonly IMapper _mapper;
        public ProvidersAPIController(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _responce = new ResponseDto();
            _mapper = mapper;
        }
        // GET: api/providers
        //[HttpGet]
        //public ResponseDto GetProviders()
        //{
        //    try
        //    {
        //        IEnumerable<HealthcareProvider> providers = _db.HealthcareProviders.ToList();
        //        _responce.Result = _mapper.Map<IEnumerable<HealthcareProviderDto>>(providers);
        //        _responce.IsSuccess = true;
        //        _responce.Message = "providers retrieved successfully.";
        //    }
        //    catch(Exception ex) 
        //    {
        //        _responce.IsSuccess = false;
        //        _responce.Message = ex.Message;
        //    }
        //    return _responce;
        //}
        [HttpGet]
        public ResponseDto GetProviders()
        {
            try
            {
                // Include the Specialty and Location entities for eager loading
                IEnumerable<HealthcareProvider> providers = _db.HealthcareProviders
                    .Include(p => p.Specialty)
                    .Include(p => p.Location)
                    .ToList();

                // Use AutoMapper to map providers to HealthcareProviderDto
                var providerDtos = _mapper.Map<IEnumerable<HealthcareProviderDto>>(providers);

                _responce.Result = providerDtos;
                _responce.IsSuccess = true;
                _responce.Message = "Providers retrieved successfully.";
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
        public  ResponseDto GetProvider(int id)
        {
            try
            {
                HealthcareProvider providers = _db.HealthcareProviders.First(u=>u.ProviderId == id);
                _responce.Result = _mapper.Map<HealthcareProviderDto>(providers);
                _responce.IsSuccess = true;
                _responce.Message = "providers retrieved successfully.";
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
        public ResponseDto CreateProvider([FromBody] HealthcareProviderDto healthcareProviderDto)
        {
            try
            {
                if (healthcareProviderDto == null)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "Invalid provider data.";
                    return _responce;
                }
                //map before adding
                HealthcareProvider provider = _mapper.Map<HealthcareProvider>(healthcareProviderDto);
                _db.HealthcareProviders.Add(provider);
                _db.SaveChanges();
                _responce.Result = healthcareProviderDto;
                _responce.IsSuccess = true;
                _responce.Message = "Provider created successfully.";

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
        public ResponseDto UpdateProvider(int id, [FromBody] HealthcareProviderDto healthcareProviderDto)
        {
            try
            {
                if (healthcareProviderDto == null) 
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "Invalid provider data.";
                    return _responce;
                }
                //retrive existing providers data from Database
                var existingData = _db.HealthcareProviders.FirstOrDefault(u => u.ProviderId == id);
                if (existingData == null) 
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "provider data not found.";
                    return _responce;
                }
                
                _mapper.Map(healthcareProviderDto, existingData);
                _db.SaveChanges();
                _responce.Result = healthcareProviderDto;
                _responce.IsSuccess = true;
                _responce.Message = "Provider updated successfully.";

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
                var existingProvider = _db.HealthcareProviders.FirstOrDefault(u => u.ProviderId == id);
                if (existingProvider == null) 
                {
                    _responce.IsSuccess = false;
                    _responce.Message = $"provider not found for id = {id}";
                    return _responce;
                }
                //remove provider from db
                _db.HealthcareProviders.Remove(existingProvider);
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
