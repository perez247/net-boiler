using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Infrastructure.Validations.Media
{
    /// <summary>
    /// Validate file
    /// </summary>
    public class FileValidator : AbstractValidator<IFormFile>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FileValidator()
        {
            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(2000000)
                .WithMessage("File size is larger 2mb");

            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png") || x.Equals("application/pdf"))
                .WithMessage("File should be jpeg, jpg, png or pdf");
        }
    }
}