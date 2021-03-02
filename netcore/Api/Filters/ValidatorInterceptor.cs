using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Api.Filters
{
    /// <summary>
    /// Validator Interception
    /// </summary>
    public class ValidatorInterceptor : IValidatorInterceptor
    {
        /// <summary>
        /// After Validation
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="commonContext"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, IValidationContext commonContext, ValidationResult result)
        {
            if (result.Errors.Count > 0)
                throw new Application.Exceptions.ValidationException(result.Errors);

            return result;
        }

        /// <summary>
        /// Before validation
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="commonContext"></param>
        /// <returns></returns>
        public IValidationContext BeforeMvcValidation(ControllerContext controllerContext, IValidationContext commonContext)
        {
            return commonContext;
        }
    }
}