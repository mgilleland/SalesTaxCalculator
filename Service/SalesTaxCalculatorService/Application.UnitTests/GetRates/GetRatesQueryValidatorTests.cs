using System;
using FluentValidation.TestHelper;
using SalesTaxCalculatorService.Application.SalesTax.Queries.GetRates;
using SalesTaxCalculatorService.Domain.Models;
using Xunit;

namespace Application.UnitTests.GetRates
{
    public class GetRatesQueryValidatorTests
    {
        private readonly GetRatesQueryValidator _sut;

        public GetRatesQueryValidatorTests()
        {
            _sut = new GetRatesQueryValidator();
        }

        [Fact]
        public void RatesRequest_can_not_be_null()
        {
            var query = new GetRatesQuery();

            var result = _sut.TestValidate(query);

            result.ShouldHaveValidationErrorFor(v => v.RatesRequest);
        }

        [Fact]
        public void Required_values_must_be_provided()
        {
            var query = new GetRatesQuery
            {
                RatesRequest = new RatesRequest(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
            };

            var result = _sut.TestValidate(query);

            result.ShouldHaveValidationErrorFor(v => v.RatesRequest.Country);
            result.ShouldHaveValidationErrorFor(v => v.RatesRequest.Zip);
        }

        [Fact]
        public void Maximum_lengths_cannot_be_exceeded()
        {
            var query = new GetRatesQuery
            {
                RatesRequest = new RatesRequest("USA", "01234567890", "FLA", string.Empty, string.Empty)
            };

            var result = _sut.TestValidate(query);

            result.ShouldHaveValidationErrorFor(v => v.RatesRequest.Country);
            result.ShouldHaveValidationErrorFor(v => v.RatesRequest.Zip);
            result.ShouldHaveValidationErrorFor(v => v.RatesRequest.State);
        }
    }
}