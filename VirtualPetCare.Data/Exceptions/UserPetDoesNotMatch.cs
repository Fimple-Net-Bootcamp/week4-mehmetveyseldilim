using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.Exceptions
{
    public class UserPetDoesNotMatch : BadRequestException
    {
        public UserPetDoesNotMatch(int userId, int petId) :
        base($"The pet with id {petId} does not belong to the user with id {userId}")
        {

        }
    }
}