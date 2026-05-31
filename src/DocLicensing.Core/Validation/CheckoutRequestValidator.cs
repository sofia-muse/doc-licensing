using DocLicensing.Core.Dtos;
using FluentValidation;

namespace DocLicensing.Core.Validation;

public class CheckoutRequestValidator : AbstractValidator<CheckoutRequest>
{
    public CheckoutRequestValidator()
    {
        RuleFor(x => x.DocumentId).GreaterThan(0);
        RuleFor(x => x.BuyerName).NotEmpty().MaximumLength(120);
        RuleFor(x => x.BuyerEmail).NotEmpty().EmailAddress();
    }
}
