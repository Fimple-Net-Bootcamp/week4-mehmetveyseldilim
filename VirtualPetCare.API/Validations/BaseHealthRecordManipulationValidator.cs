using FluentValidation;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.Entities;

namespace VirtualPetCare.API.Validations
{
    public class UpdateHealthRecordDTOValidator : AbstractValidator<UpdateHealthRecordDTO>
    {
        public UpdateHealthRecordDTOValidator()
        {
            RuleFor(hr => hr.GeneralHealth)
                .Must(BeAValidEnum).WithMessage("Invalid health status.");

            RuleFor(hr => hr.LastVaccinationDate)
                .Must(BeAValidDate).WithMessage("Invalid date format for last vaccination date.");

            RuleFor(hr => hr.Weight)
                .Must(BeAValidWeight).WithMessage("Weight must be a valid number.");
        }

        private bool BeAValidEnum(string? generalHealth)
        {
            // If generalHealth is null or empty, it's considered valid
            if (string.IsNullOrWhiteSpace(generalHealth))
            {
                return true;
            }

            // Try to parse the string to the HealthStatus enum
            return Enum.TryParse<HealthStatus>(generalHealth, ignoreCase: true, out _);
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