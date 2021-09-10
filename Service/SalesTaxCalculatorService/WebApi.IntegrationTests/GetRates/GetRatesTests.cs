using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using SalesTaxCalculatorService.Domain.Models.Rates;
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
        public async Task Valid_US_request_returns_expected_results()
        {
            using var httpClient = _webApplicationFactory.CreateClient();

            var response = await httpClient.GetAsync("api/v1.0/rates?country=US&zip=33614");

            response.IsSuccessStatusCode.ShouldBeTrue();

            var rates = await IntegrationTestHelper.GetResponseContent<UsTaxRates>(response);
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

        [Fact]
        public async Task Valid_CA_request_returns_expected_results()
        {
            using var httpClient = _webApplicationFactory.CreateClient();

            var response = await httpClient.GetAsync("api/v1.0/rates?country=CA&zip=V5K0A1");

            response.IsSuccessStatusCode.ShouldBeTrue();

            var rates = await IntegrationTestHelper.GetResponseContent<CaTaxRates>(response);
            rates.City.ShouldBe("Vancouver");
            rates.Country.ShouldBe("CA");
            rates.CombinedRate.ShouldBe(0.12F);
            rates.State.ShouldBe("BC");
            rates.FreightTaxable.ShouldBeTrue();
        }

        [Fact]
        public async Task Valid_AU_request_returns_expected_results()
        {
            using var httpClient = _webApplicationFactory.CreateClient();

            var response = await httpClient.GetAsync("api/v1.0/rates?country=AU&zip=2000");

            response.IsSuccessStatusCode.ShouldBeTrue();

            var rates = await IntegrationTestHelper.GetResponseContent<AuTaxRates>(response);
            rates.Zip.ShouldBe("2000");
            rates.Country.ShouldBe("AU");
            rates.CombinedRate.ShouldBe(0.1F);
            rates.CountryRate.ShouldBe(0.1F);
            rates.FreightTaxable.ShouldBeTrue();
        }

        [Fact]
        public async Task Valid_EU_request_returns_expected_results()
        {
            using var httpClient = _webApplicationFactory.CreateClient();

            var response = await httpClient.GetAsync("api/v1.0/rates?country=FI&zip=00150");

            response.IsSuccessStatusCode.ShouldBeTrue();

            var rates = await IntegrationTestHelper.GetResponseContent<EuTaxRates>(response);
            rates.Country.ShouldBe("FI");
            rates.DistanceSaleThreshold.ShouldBe(0);
            rates.Name.ShouldBe("Finland");
            rates.ParkingRate.ShouldBe(0);
            rates.ReducedRate.ShouldBe(0.14F);
            rates.StandardRate.ShouldBe(0.24F);
            rates.FreightTaxable.ShouldBeTrue();
        }
    }
}