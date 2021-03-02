using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Infrastructure.Validations.Media
{
    /// <summary>
    /// Base 64 validator
    /// </summary>
    public class ImageBase64Validator : AbstractValidator<string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ImageBase64Validator()
        {
            RuleFor(x => x).NotNull()
                .WithMessage("Image is required")
                .NotEmpty()
                .WithMessage("Image is required");

            RuleFor(x => x)
                .MustAsync(async (x, y) => await FileValidation.BeAValidPicture(x)).WithMessage("File is not an image - png or jpeg")
                .Must((x, y) => FileValidation.BeOfValidSize(x)).WithMessage("Image must be less than 2mb");
        }
    
    }
}