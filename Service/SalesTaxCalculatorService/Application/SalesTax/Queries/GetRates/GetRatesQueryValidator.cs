using FluentValidation;

namespace SalesTaxCalculatorService.Application.SalesTax.Queries.GetRates
{
    public class GetRatesQueryValidator : AbstractValidator<GetRatesQuery>
    {
        public GetRatesQueryValidator()
        {
            RuleFor(v => v.RatesRequest).NotNull().DependentRules(() =>
            {
                RuleFor(v => v.RatesRequest.Country).NotEmpty().MaximumLength(2);
                RuleFor(v => v.RatesRequest.Zip).NotEmpty().MaximumLength(10);
                RuleFor(v => v.RatesRequest.State).MaximumLength(2);
            });
        }
    }
}