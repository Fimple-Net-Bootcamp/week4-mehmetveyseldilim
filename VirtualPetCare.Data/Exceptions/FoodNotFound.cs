using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.Exceptions
{

    public sealed class FoodNotFound : BadRequestException
    {
        public FoodNotFound(int foodId) : base($"Food with id: {foodId} doesn't exist in the database")
        {
            
        }
    }
    
}