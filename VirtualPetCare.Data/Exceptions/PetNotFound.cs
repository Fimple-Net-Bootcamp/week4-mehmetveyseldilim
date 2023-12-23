using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.Exceptions
{
    public sealed class PetNotFound : BadRequestException
    {
        public PetNotFound(int petId) : base($"Pet with id: {petId} doesn't exist in the database")
        {
            
        }
    }
}