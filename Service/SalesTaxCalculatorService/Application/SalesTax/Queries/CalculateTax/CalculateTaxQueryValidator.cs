using FluentValidation;
using System.Collections.Generic;

namespace SalesTaxCalculatorService.Application.SalesTax.Queries.CalculateTax
{
    public class CalculateTaxQueryValidator : AbstractValidator<CalculateTaxQuery>
    {
        private readonly List<string> _taxExemptionType = new()
            {"wholesale", "government", "marketplace", "other", "non_exempt"};

        public CalculateTaxQueryValidator()
        {
            RuleFor(v => v.FromCountry).MaximumLength(2);
            RuleFor(v => v.FromZip).MaximumLength(10);
            RuleFor(v => v.FromState).MaximumLength(2);
            RuleFor(v => v.ToCountry).NotEmpty().MaximumLength(2);
            RuleFor(v => v.ToZip).MaximumLength(10);
            RuleFor(v => v.ToZip).NotEmpty().When(v => v.ToCountry == "US");
            RuleFor(v => v.ToState).MaximumLength(2);
            RuleFor(v => v.ToState).NotEmpty().When(v => v.ToCountry is "US" or "CA");
            RuleFor(v => v.Amount).NotEmpty();
            RuleFor(v => v.TaxExemptionType).Must(BeValidTaxExemptionType).WithMessage("An invalid tax exemption type was sent");
        }

        private bool BeValidTaxExemptionType(string taxExemptionType)
        {
            return string.IsNullOrWhiteSpace(taxExemptionType) || _taxExemptionType.Contains(taxExemptionType);
        }
    }
}