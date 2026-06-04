using DocLicensing.Core.Dtos;
using FluentValidation;

namespace DocLicensing.Core.Validation;

public class SearchQueryValidator : AbstractValidator<SearchQuery>
{
    public SearchQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        RuleFor(x => x.Q).MaximumLength(200);
        RuleFor(x => x.Category).MaximumLength(100);
        RuleFor(x => x.Lang).Must(l => l is "en" or "he").WithMessage("Lang must be 'en' or 'he'.");
    }
}
