using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Infrastructure.CustomAnnotations
{
    /// <summary>
    /// Converts non intergers to 0
    /// </summary>
     [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DefaultNumberAnnotation :  ValidationAttribute
    {

        private readonly int _defaultCountry;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <returns></returns>
        public DefaultNumberAnnotation(int defaultCountry) : base("")
        {
            _defaultCountry = defaultCountry;
        }

        /// <summary>
        /// Valid the field
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var thisProperty = validationContext.ObjectType.GetProperty(validationContext.MemberName);

            
            var propertyValue = (string)thisProperty.GetValue(validationContext.ObjectInstance, null);
            
            // throw new Exception("failed");

            if (!Int32.TryParse(propertyValue, out Int32 result))
                return null;
                
            thisProperty.SetValue(validationContext.ObjectInstance, _defaultCountry);
            return null;

         }
    }
}