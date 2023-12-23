using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.Exceptions
{
    public sealed class NoPetTypeFound : BadRequestException
    {
        public NoPetTypeFound(string petTypeName) : base($"No pet type with name {petTypeName} has been found")
        {
            
        }

        public NoPetTypeFound(int petTypeId) : base($"No pet type with id {petTypeId} has been found")
        {
            
        }
    }
}