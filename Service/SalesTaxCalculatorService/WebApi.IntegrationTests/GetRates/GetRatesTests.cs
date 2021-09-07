using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using SalesTaxCalculatorService.Domain.Models;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace SalesTaxCalculatorService.WebApi.IntegrationTests.GetRates
{
    public class GetRatesTests
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        public GetRatesTests()
        {
            _webApplicationFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(
                    builder =>
                    {
                        builder.ConfigureTestServices(_ => { });
                    });
        }

        [Fact]
        public async Task Invalid_country_is_unsuccessful()
        {
            using var httpClient = _webApplicationFactory.CreateClient();

            var response = await httpClient.GetAsync("api/v1.0/rates?country=XX");
            response.IsSuccessStatusCode.ShouldBeFalse();
        }

        [Fact]
        public async Task Valid_request_returns_expected_results()
        {
            using var httpClient = _webApplicationFactory.CreateClient();

            var response = await httpClient.GetAsync("api/v1.0/rates?country=US&zip=33614");

            response.IsSuccessStatusCode.ShouldBeTrue();

            var rates = await IntegrationTestHelper.GetResponseContent<TaxRates>(response);
            rates.State.ShouldBe("FL");
            rates.City.ShouldBe("EGYPT LAKE-LETO");
            rates.Country.ShouldBe("US");
            rates.CombinedRate.ShouldBe(0.075F);
            rates.County.ShouldBe("HILLSBOROUGH");
            rates.CountyRate.ShouldBe(0.015F);
            rates.StateRate.ShouldBe(0.06F);
            rates.Zip.ShouldBe("33614");
            rates.FreightTaxable.ShouldBeFalse();
        }
    }
}