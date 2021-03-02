using System;
using System.ComponentModel.DataAnnotations;
using Application.Infrastructure.Token;

namespace Application.Infrastructure.GenericCommands.Pagination
{
    /// <summary>
    /// Entity to send back to user
    /// </summary>
    public class PaginationCommand : TokenCredentials
    {
        /// <summary>
        /// The Page number maximum of 20000 and minimum of 1
        /// </summary>
        /// <value></value>
        
        [PaginationSize(1, 20000, 1)]
        public int PageNumber { get; set; }

        /// <summary>
        /// The page size maximum of 20 and minimum of 1
        /// </summary>
        /// <value></value>
        
        [PaginationSize(1, 20, 10)]
        public int PageSize { get; set; }
    }

    /// <summary>
    /// Defines the pagination size
    /// </summary>    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PaginationSize :  ValidationAttribute
    {
        private readonly int _lowest;
        private readonly int _highest;
        private readonly int _defaultValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Lowest"></param>
        /// <param name="Highest"></param>
        /// <param name="DefaultValue"></param>
        public PaginationSize(int Lowest, int Highest, int DefaultValue)
        : base("")
        {
            _lowest = Lowest;
            _highest = Highest;
            _defaultValue = DefaultValue;
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

            // if (value == null){
            //     thisProperty.SetValue(validationContext.ObjectInstance, 1);
            //     return null;
            // }

            
            var propertyValue = (int?)thisProperty.GetValue(validationContext.ObjectInstance, null);

            if(propertyValue.HasValue && propertyValue.Value >= _lowest &&  propertyValue.Value <= _highest)
                return null;
                
            thisProperty.SetValue(validationContext.ObjectInstance, _defaultValue);
            return null;

        }
    }
}