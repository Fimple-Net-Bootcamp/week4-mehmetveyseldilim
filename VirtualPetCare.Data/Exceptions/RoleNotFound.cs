using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.Exceptions
{
    public sealed class RoleNotFound : BadRequestException
    {
        public RoleNotFound(string role) : base($"Role with name: {role} doesn't exist in the database")
        {
            
        }
    }
}