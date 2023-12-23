using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualPetCare.API.ActionFilters;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;

namespace VirtualPetCare.API.Controllers
{
    [ApiController]
    [Route("api/pets")]
    public class PetsController : ControllerBase
    {

        private readonly IPetRepository _petRepository;

        private readonly ILogger<PetsController> _logger;

        public PetsController(IPetRepository petRepository, ILogger<PetsController> logger)
        {
            _logger = logger;
            _petRepository = petRepository;
        }


        

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ReadPetDTO>>> GetAllPets()
        {
            _logger.LogDebug($"In HTTP GET {nameof(GetAllPets)} method");

            var pets = await _petRepository.GetAllPetsAsync();

            _logger.LogDebug("Returning all pets => {@pets}", pets);

            return Ok(pets);
        }

        [HttpGet("{petId}", Name = $"{nameof(GetPetById)}")]
        public async Task<ActionResult<ReadPetDTO>> GetPetById(int petId) 
        {
            var pet = await _petRepository.GetPetByIdAsync(petId);

            return Ok(pet);
        }

        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(FluentValidationFilter))]
        public async Task<ActionResult<ReadPetDTO>>  CreatePet(CreatePetDTO createPetDTO)
        {
            _logger.LogDebug($"In HTTP POST {nameof(CreatePet)} method");

            int currentUserId = GetCurrentUserId();

            if(currentUserId == -1) 
            {
                _logger.LogError("Unable to parse user id to int! {@currentUserId}", currentUserId);
                return BadRequest($"User id {currentUserId} is not integer!");
            }

       
            var createdPet = await _petRepository.CreatePetAsync(createPetDTO, currentUserId);

            return Ok(createdPet);
   

        }


        [HttpPut("{petId}")]
        [Authorize]
        [ServiceFilter(typeof(FluentValidationFilter))]
        public async Task<ActionResult<ReadPetDTO>>  UpdatePet(int petId, UpdatePetDTO updatePetDTO)
        {
            _logger.LogDebug($"In HTTP PUT {nameof(UpdatePet)} method");

            int currentUserId = GetCurrentUserId();

            if(currentUserId == -1) 
            {
                _logger.LogError("Unable to parse user id to int! {@currentUserId}", currentUserId);
                return BadRequest($"User id {currentUserId} is not integer!");
            }

            var createdPet = await _petRepository.UpdatePetAsync(currentUserId, petId, updatePetDTO);

            return Ok(createdPet);

        }

        private int GetCurrentUserId()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _logger.LogDebug($"Current User Id is: {@currentUserId}", currentUserId);

            if(int.TryParse(currentUserId, out int userId)) 
            {
                return userId;
            }

            return -1;
        }
        
        
        
    }
}