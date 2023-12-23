using FluentValidation;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.Entities;

namespace VirtualPetCare.API.Validations
{
    public class CreatePetDTOValidator : AbstractValidator<CreatePetDTO>
    {   
        public CreatePetDTOValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Pet name is required")
            .Length(3,25).WithMessage("Pet name length must be between 3 and 25");

            RuleFor(p => p.PetTypeName)
            .NotEmpty().WithMessage("Pet type must be spesified")
            .IsEnumName(typeof(PetTypeEnum), caseSensitive: false)
            .WithMessage("PetType must be a valid value from the PetTypeEnum");

            RuleFor(hr => hr.GeneralHealth)
                .NotEmpty().WithMessage("General health must not be empty.")
                .IsEnumName(typeof(HealthStatus), caseSensitive: false).WithMessage("Invalid health status.");

            RuleFor(hr => hr.LastVaccinationDate)
                .Must(BeAValidDate).WithMessage("Invalid date format for last vaccination date.");

            RuleFor(hr => hr.Weight)
                .Must(BeAValidWeight).WithMessage("Weight must be a valid number.");
    
        }

        private bool BeAValidDate(DateTime? date)
        {
            return !date.HasValue || date.Value.Year > 2000; 
        }

        private bool BeAValidWeight(double? weight)
        {
            return !weight.HasValue || weight.Value > 0; 
        }
    }
}