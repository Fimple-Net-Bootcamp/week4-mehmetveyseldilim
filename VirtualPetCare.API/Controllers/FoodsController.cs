using Microsoft.AspNetCore.Mvc;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;

namespace VirtualPetCare.API.Controllers
{
    [ApiController]
    [Route("api/foods")]
    public class FoodsController : ControllerBase
    {

        private readonly IFoodRepository _foodRepository;

        public FoodsController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadFoodDTO>>> GetAllFoods()
        {
            var readFoodDTOs = await _foodRepository.GetAllFoodsAsync();

            return Ok(readFoodDTOs);
        }

        [HttpPost("{petId}/{foodId}")]
        public async Task<IActionResult> FeedThePet(int petId, int foodId) 
        {
            var boole = await _foodRepository.FeedThePet(petId, foodId);

            return Ok(boole);
        }
    }
}