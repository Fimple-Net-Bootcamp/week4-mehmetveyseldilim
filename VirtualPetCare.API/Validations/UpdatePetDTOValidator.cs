using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using VirtualPetCare.Data.DTOs;

namespace VirtualPetCare.API.Validations
{
    public class UpdatePetDTOValidator : AbstractValidator<UpdatePetDTO>
    {
        public UpdatePetDTOValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Pet name is required")
            .Length(3,25).WithMessage("Pet name length must be between 3 and 25");
        }
    }
}