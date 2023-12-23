using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using VirtualPetCare.Data.Entities;

namespace VirtualPetCare.API.Helper
{
    public static class ErrorDetailsHelper
    {
        public static ErrorDetails CreateErrorDetails(ValidationResult validationResult)
        {
            var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

            return new ErrorDetails()
            {
                StatusCode = 400,
                Message = errorMessages
            };
        }
    }
}