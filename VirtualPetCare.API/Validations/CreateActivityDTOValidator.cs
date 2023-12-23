using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.Entities;

namespace VirtualPetCare.API.Validations
{
    public class CreateActivityDTOValidator : AbstractValidator<CreateActivityDTO>
    {
        public CreateActivityDTOValidator()
        {
            RuleFor(x => x.ActivityName)
            .NotEmpty().WithMessage("Activity name cannot be empty");

            RuleFor(x => x.PetTypes)
                .Must(BeAValidPetTypeName)
                .WithMessage("Invalid pet types in the collection");

        }

        private bool BeAValidPetTypeName(ICollection<string> petTypeNames) 
        {
            if (petTypeNames == null || petTypeNames.Count == 0)
            {
                return false;
            }

            // Check if each string element can be parsed to the PetTypeEnum
            return petTypeNames.All(type => Enum.TryParse<PetTypeEnum>(type, ignoreCase: true, out _));

        }
    }
}