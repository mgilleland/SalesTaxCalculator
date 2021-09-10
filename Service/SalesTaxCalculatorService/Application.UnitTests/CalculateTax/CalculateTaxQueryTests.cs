using SalesTaxCalculatorService.Application.Common.Exceptions;
using SalesTaxCalculatorService.Application.Common.Interfaces;
using SalesTaxCalculatorService.Application.SalesTax.Queries.CalculateTax;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SalesTaxCalculatorService.Application.UnitTests.CalculateTax
{
    [Collection("TaxTests")]
    public class CalculateTaxQueryTests
    {
        private readonly ITaxCalculatorFactory _taxCalculatorFactory;
        private readonly ICustomerRepository _customerRepository;

        public CalculateTaxQueryTests(TaxTestFixture fixture)
        {
            _customerRepository = fixture?.CustomerRepository;
            _taxCalculatorFactory = fixture?.TaxCalculatorFactory;
        }

        [Fact]
        public async Task Valid_request_returns_expected_results()
        {
            var sut = new CalculateTaxQuery.CalculateTaxQueryHandler(_taxCalculatorFactory, _customerRepository);

            var query = new CalculateTaxQuery
            {
                CustomerId = 1,
                Amount = 10,
                Shipping = 1,
                FromCountry = "US",
                FromState = "FL",
                FromZip = "33614",
                ToCountry = "US",
                ToState = "FL",
                ToZip = "33614"
            };

            var response = await sut.Handle(query, CancellationToken.None);

            response.State.ShouldBe("FL");
            response.City.ShouldBe("Tampa");
            response.Country.ShouldBe("US");
            response.County.ShouldBe("Hillsborough");
            response.TaxSource.ShouldBe("destination");
            response.AmountToCollect.ShouldBe(0.75f);
            response.Rate.ShouldBe(0.075f);
            response.OrderTotalAmount.ShouldBe(10);
            response.Shipping.ShouldBe(1);
            response.FreightTaxable.ShouldBeFalse();
        }

        [Fact]
        public void Invalid_customerId_throws_error()
        {
            var sut = new CalculateTaxQuery.CalculateTaxQueryHandler(_taxCalculatorFactory, _customerRepository);

            var query = new CalculateTaxQuery
            {
                CustomerId = 99,
                Amount = 10,
                Shipping = 1,
                FromCountry = "US",
                FromState = "FL",
                FromZip = "33614",
                ToCountry = "US",
                ToState = "FL",
                ToZip = "33614"
            };

            Should.Throw<NotFoundException>(async () =>
            {
                await sut.Handle(query, CancellationToken.None);
            });
        }
    }
}