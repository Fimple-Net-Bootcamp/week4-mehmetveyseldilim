using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.Exceptions
{
    public sealed class UserNotFound : BadRequestException
    {
    
        public UserNotFound(string id) : base($"User with name: {id} doesn't exist in the database")
        {
            
        }

        public UserNotFound(int id) : base($"User with name: {id} doesn't exist in the database")
        {
            
        }
    }
    
}