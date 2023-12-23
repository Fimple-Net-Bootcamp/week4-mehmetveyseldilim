using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VirtualPetCare.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        //. Navigation Properties
        public ICollection<Pet>? Pets {get; set;}

        public virtual required ICollection<ApplicationUserRole> UserRoles {get; set;}

    }
}