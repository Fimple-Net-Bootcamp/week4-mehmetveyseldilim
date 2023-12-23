using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualPetCare.Data.BaseValidationModel;

namespace VirtualPetCare.Data.DTOs
{
    public class UpdatePetDTO : BaseValidationModel<UpdatePetDTO>
    {
        public required string Name {get; set;}

    }
}