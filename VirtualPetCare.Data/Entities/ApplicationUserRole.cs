using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VirtualPetCare.Data.Entities
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public virtual required User User {get; set;}

        public virtual required CustomIdentityRole Role {get; set;}
    }
}