using Microsoft.AspNetCore.Mvc;
using VirtualPetCare.API.ActionFilters;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;

namespace VirtualPetCare.API.Controllers
{
    [ApiController]
    [Route("api/activities")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ILogger<ActivitiesController> _logger;

        public ActivitiesController(IActivityRepository activityRepository, ILogger<ActivitiesController> logger)
        {
            _activityRepository = activityRepository;
            _logger = logger;
        }

        [HttpPost]
        [ServiceFilter(typeof(FluentValidationFilter))]
        public async Task<ActionResult<ReadActivityDTO>> CreateActivity(CreateActivityDTO createActivityDTO) 
        {
            _logger.LogDebug($"In Activities Controller {nameof(CreateActivity)} method");
            _logger.LogDebug("Create Activity Dto: {@createActivityDTO}", createActivityDTO);

            var addedAcivityReadDTO = await _activityRepository.CreateActivityAsync(createActivityDTO);

            return Ok(addedAcivityReadDTO);
        }

        [HttpGet("{petId}")]
        public async Task<ActionResult<IEnumerable<ReadActivityDTO>>> GetActivitiesForPet(int petId) 
        {
            var readActivityDTOList = await _activityRepository.GetActivitiesForSpesificPetAsync(petId);

            return Ok(readActivityDTOList);
        }
    }
}