using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VirtualPetCare.Data.Entities
{
    public class CustomIdentityRole : IdentityRole<int>
    {
        public virtual required ICollection<ApplicationUserRole> UserRoles {get; set;}

    }
}