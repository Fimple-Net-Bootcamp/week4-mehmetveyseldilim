using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.Exceptions
{
    public class PetFoodDoesNotMatch : BadRequestException
    {
        public PetFoodDoesNotMatch(int petId, int foodId) :
        base($"The pet with id {petId} does not eat the food with id {foodId}")
        {

        }
    }
}