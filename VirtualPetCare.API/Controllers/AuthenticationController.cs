using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;

namespace VirtualPetCare.API.Controllers
{
    [ApiController]
    [Route("api/kullanicilar")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationRepository _authenticationRepository;

        private readonly IPetRepository _petRepository;


        public AuthenticationController(IAuthenticationRepository authenticationRepository, 
        IPetRepository petRepository)
        {
            _authenticationRepository = authenticationRepository;
            _petRepository = petRepository;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUserById(int id) 
        {
            var user = await _authenticationRepository.GetUserByIdAsync(id);

            return Ok(user);
        }

        [HttpPost]
        // [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _authenticationRepository.RegisterUser(userForRegistration);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        [HttpPost("login")]
        // [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authenticationRepository.ValidateUser(user))
                return Unauthorized();

            var tokenDto = await _authenticationRepository.CreateToken(populateExp: true);

            return Ok(tokenDto);
        }

        [HttpGet("istatistikler/{userId}")]
        public async Task<IActionResult> GetUserPetStatistics(int userId)
        {
            var petWithStatistics = await _petRepository.GetPetsByUserIdAsync(userId);

            return Ok(petWithStatistics);
        }


        
    }
}