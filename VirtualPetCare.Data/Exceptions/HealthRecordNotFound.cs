using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.Exceptions
{
    public class HealthRecordNotFound : BadRequestException
    {
        public HealthRecordNotFound(int petId) : base($"Health Record with pet id {petId} does not exist in the database")
        {
            
        }
    }
}