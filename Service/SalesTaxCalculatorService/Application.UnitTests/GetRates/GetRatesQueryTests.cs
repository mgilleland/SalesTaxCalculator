using SalesTaxCalculatorService.Application.Common.Exceptions;
using SalesTaxCalculatorService.Application.Common.Interfaces;
using SalesTaxCalculatorService.Application.SalesTax.Queries.GetRates;
using SalesTaxCalculatorService.Domain.Models;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SalesTaxCalculatorService.Application.UnitTests.GetRates
{
    [Collection("TaxTests")]
    public class GetRatesQueryTests
    {
        private readonly ITaxCalculatorFactory _taxCalculatorFactory;
        private readonly ICustomerRepository _customerRepository;

        public GetRatesQueryTests(TaxTestFixture fixture)
        {
            _customerRepository = fixture?.CustomerRepository;
            _taxCalculatorFactory = fixture?.TaxCalculatorFactory;
        }

        [Fact]
        public async Task Valid_request_returns_expected_results()
        {
            var sut = new GetRatesQuery.GetRatesQueryHandler(_taxCalculatorFactory, _customerRepository);

            var query = new GetRatesQuery
            {
                CustomerId = 1,
                RatesRequest = new RatesRequest("US", "33614", string.Empty, string.Empty, string.Empty)
            };

            var response = await sut.Handle(query, CancellationToken.None);

            response.State.ShouldBe("Florida");
            response.City.ShouldBe("Tampa");
            response.Country.ShouldBe("US");
            response.CombinedRate.ShouldBe(0.075F);
            response.County.ShouldBe("Hillsborough");
            response.CountyRate.ShouldBe(0.015F);
            response.StateRate.ShouldBe(0.06F);
            response.Zip.ShouldBe("33614");
            response.FreightTaxable.ShouldBeFalse();
        }

        [Fact]
        public void Invalid_customerId_throws_error()
        {
            var sut = new GetRatesQuery.GetRatesQueryHandler(_taxCalculatorFactory, _customerRepository);

            var query = new GetRatesQuery
            {
                CustomerId = 99,
                RatesRequest = new RatesRequest("US", "33558", string.Empty, string.Empty, string.Empty)
            };

            Should.Throw<NotFoundException>(async () =>
            {
                await sut.Handle(query, CancellationToken.None);
            });
        }
    }
}