using FluentValidation;
using UrlShortener.WebApplication.Models;

namespace UrlShortener.WebApplication.Validators
{
    public class ShortenerDTOValidator : AbstractValidator<ShorteningViewModel>
    {
        public ShortenerDTOValidator()
        {
            RuleFor(v => v.TargetUrl).Must(v => UrlValidator.Validate(v)).WithMessage("The value is not a valid uri.");
        }

    }
}
