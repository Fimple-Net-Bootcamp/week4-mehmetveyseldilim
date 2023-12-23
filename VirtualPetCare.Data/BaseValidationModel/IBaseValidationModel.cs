using FluentValidation.Results;

namespace VirtualPetCare.Data.BaseValidationModel
{
    public interface IBaseValidationModel
    {
        public ValidationResult? Validate(object validator, IBaseValidationModel modelObj);
    }
}