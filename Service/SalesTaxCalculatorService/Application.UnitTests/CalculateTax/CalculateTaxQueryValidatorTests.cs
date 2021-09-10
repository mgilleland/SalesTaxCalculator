using FluentValidation.TestHelper;
using SalesTaxCalculatorService.Application.SalesTax.Queries.CalculateTax;
using Xunit;

namespace SalesTaxCalculatorService.Application.UnitTests.CalculateTax
{
    public class CalculateTaxQueryValidatorTests
    {
        private readonly CalculateTaxQueryValidator _sut;

        public CalculateTaxQueryValidatorTests()
        {
            _sut = new CalculateTaxQueryValidator();
        }

        [Fact]
        public void Required_values_must_be_provided()
        {
            var query = new CalculateTaxQuery();

            var result = _sut.TestValidate(query);

            result.ShouldHaveValidationErrorFor(v => v.ToCountry);
            result.ShouldHaveValidationErrorFor(v => v.Amount);
        }

        [Fact]
        public void Maximum_lengths_cannot_be_exceeded()
        {
            var query = new CalculateTaxQuery
            {
                FromCountry = "USA",
                FromZip = "01234567890",
                FromState = "FLA",
                ToCountry = "USA",
                ToZip = "01234567890",
                ToState = "FLA"
            };

            var result = _sut.TestValidate(query);

            result.ShouldHaveValidationErrorFor(v => v.FromCountry);
            result.ShouldHaveValidationErrorFor(v => v.FromZip);
            result.ShouldHaveValidationErrorFor(v => v.FromState);
            result.ShouldHaveValidationErrorFor(v => v.ToCountry);
            result.ShouldHaveValidationErrorFor(v => v.ToZip);
            result.ShouldHaveValidationErrorFor(v => v.ToState);
        }

        [Fact]
        public void US_request_requires_zip_and_state()
        {
            var query = new CalculateTaxQuery
            {
                ToCountry = "US"
            };

            var result = _sut.TestValidate(query);

            result.ShouldHaveValidationErrorFor(v => v.ToZip);
            result.ShouldHaveValidationErrorFor(v => v.ToState);
        }

        [Fact]
        public void CA_request_requires_state()
        {
            var query = new CalculateTaxQuery
            {
                ToCountry = "CA"
            };

            var result = _sut.TestValidate(query);

            result.ShouldHaveValidationErrorFor(v => v.ToState);
        }

        [Fact]
        public void Invalid_TaxExemptionType_returns_an_error()
        {
            var query = new CalculateTaxQuery
            {
                ToCountry = "US",
                TaxExemptionType = "BadType"
            };

            var result = _sut.TestValidate(query);

            result.ShouldHaveValidationErrorFor(v => v.TaxExemptionType);
        }

        [Fact]
        public void Valid_TaxExemptionType_is_accepted()
        {
            var query = new CalculateTaxQuery
            {
                ToCountry = "US",
                TaxExemptionType = "non_exempt"
            };

            var result = _sut.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(v => v.TaxExemptionType);
        }
    }
}