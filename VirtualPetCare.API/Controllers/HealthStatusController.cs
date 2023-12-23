using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VirtualPetCare.API.ActionFilters;
using VirtualPetCare.API.Helper;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;

namespace VirtualPetCare.API.Controllers
{
    [ApiController]
    [Route("api/healthstatuses")]
    public class HealthStatusesController  : ControllerBase
    {

        private readonly IHealthRepository _healthRepository;

        private readonly ILogger<HealthStatusesController> _logger;

        private readonly IValidator<UpdateHealthRecordDTO> _validator;

        public HealthStatusesController(IHealthRepository healthRepository, ILogger<HealthStatusesController> logger, IValidator<UpdateHealthRecordDTO> validator)
        {
            _healthRepository = healthRepository;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet("{petId}")]
        public async Task<ActionResult<ReadHealthRecordDTO>> GetHealthRecordByPetId(int petId) 
        {
            _logger.LogDebug($"In HealthStatus Controller {nameof(GetHealthRecordByPetId)})");

            _logger.LogDebug("Pet id: {@petId}", petId);

            var mappedHealthRecord = await _healthRepository.GetHealthRecordForSpesificPetAsync(petId);

            _logger.LogInformation("Returning health record: {@mappedHealthRecord}", mappedHealthRecord);

            return Ok(mappedHealthRecord);
        }


        [HttpPatch("{petId}")]
        public  async Task<IActionResult> UpdateHealthRecordByPetIdByPatch(
            int petId, [FromBody] JsonPatchDocument<UpdateHealthRecordDTO> jsonPatchDocument)
        {
            _logger.LogDebug($"In HealthStatus Controller {nameof(UpdateHealthRecordByPetIdByPatch)})");
            _logger.LogDebug("Pet Id: {@petId}", petId);

            var updateHealthRecordDto = new UpdateHealthRecordDTO();

            jsonPatchDocument.ApplyTo(updateHealthRecordDto);

            _logger.LogDebug("After json patch apply method: {@updateHealthRecordDto}", updateHealthRecordDto);

            var validationResult = _validator.Validate(updateHealthRecordDto);

            if(!validationResult.IsValid)
            {
                var errorDetails = ErrorDetailsHelper.CreateErrorDetails(validationResult);
                _logger.LogError("Validation failed! Returning error details: {@errorDetails}", errorDetails);
                return BadRequest(errorDetails);
            }

            var readHealthRecordDTO = await _healthRepository.UpdateHealthRecordForSpesificPetAsync(petId, updateHealthRecordDto);

           
            return Ok(readHealthRecordDTO);

        }

        // [HttpPost("{petId}")]
        // [ServiceFilter(typeof(FluentValidationFilter))]
        // public async Task<ActionResult<ReadHealthRecordDTO>> CreateHealthRecordByPetId(int petId, CreateHealthRecord createHealthRecord)
        // {
        //     _logger.LogDebug($"In HealthStatus Controller {nameof(GetHealthRecordByPetId)})");

        //     _logger.LogDebug("Pet id: {@petId}", petId);

        //     _logger.
        // }
    }
}