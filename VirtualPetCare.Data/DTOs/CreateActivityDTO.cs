using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VirtualPetCare.Data.BaseValidationModel;

namespace VirtualPetCare.Data.DTOs
{
    public class CreateActivityDTO : BaseValidationModel<CreateActivityDTO>
    {
        public required string ActivityName {get; set;}
        public required ICollection<string> PetTypes {get; set;}
    }
}

