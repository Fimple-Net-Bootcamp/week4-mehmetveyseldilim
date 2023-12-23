
using FluentValidation;
using FluentValidation.Results;

namespace VirtualPetCare.Data.BaseValidationModel
{
    public class BaseValidationModel<T> : IBaseValidationModel
    {
        public ValidationResult? Validate(object validator, IBaseValidationModel modelObj)
        {
            var instance = validator as IValidator<T>;     

            var result = instance?.Validate((T)modelObj);
        
            return result;
        }
    }
}