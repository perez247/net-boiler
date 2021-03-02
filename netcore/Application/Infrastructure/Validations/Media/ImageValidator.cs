using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Infrastructure.Validations.Media
{
    /// <summary>
    /// mage validator
    /// </summary>
    public class ImageValidator : AbstractValidator<IFormFile>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ImageValidator()
        {
            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(2000000)
                .WithMessage("File size is larger 2mb");

            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("File should be jpeg, jpg or png");
        }
    }
}