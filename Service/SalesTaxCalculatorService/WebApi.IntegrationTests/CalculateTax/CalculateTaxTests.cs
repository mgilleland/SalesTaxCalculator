using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using SalesTaxCalculatorService.Application.SalesTax.Queries.CalculateTax;
using SalesTaxCalculatorService.Domain.Models.Taxes;
using Shouldly;
using Xunit;

namespace SalesTaxCalculatorService.WebApi.IntegrationTests.CalculateTax
{
    public class CalculateTaxTests
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        public CalculateTaxTests()
        {
            _webApplicationFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(
                    builder =>
                    {
                        builder.ConfigureTestServices(_ => { });
                    });
        }

        [Fact]
        public async Task Valid_US_request_returns_expected_results()
        {
            using var httpClient = _webApplicationFactory.CreateClient();

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
                ToZip = "33558"
            };

            var requestContent = IntegrationTestHelper.GetRequestContent(query);

            var response = await httpClient.PostAsync("api/v1.0/taxes", requestContent);

            response.IsSuccessStatusCode.ShouldBeTrue();

            var taxes = await IntegrationTestHelper.GetResponseContent<OrderTaxes>(response);

            taxes.OrderTotalAmount.ShouldBe(11);
            taxes.TaxableAmount.ShouldBe(10);
            taxes.Rate.ShouldBe(.075f);
            taxes.Shipping.ShouldBe(1);
            taxes.AmountToCollect.ShouldBe(.75f);
            taxes.TaxSource.ShouldBe("destination");
            taxes.Country.ShouldBe("US");
            taxes.State.ShouldBe("FL");
            taxes.County.ShouldBe("HILLSBOROUGH");
            taxes.City.ShouldBe("CHEVAL");
            taxes.FreightTaxable.ShouldBeFalse();
        }

        [Fact]
        public async Task Wholesale_request_returns_expected_results()
        {
            using var httpClient = _webApplicationFactory.CreateClient();

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
                ToZip = "33558",
                TaxExemptionType = "wholesale"
            };

            var requestContent = IntegrationTestHelper.GetRequestContent(query);

            var response = await httpClient.PostAsync("api/v1.0/taxes", requestContent);

            response.IsSuccessStatusCode.ShouldBeTrue();

            var taxes = await IntegrationTestHelper.GetResponseContent<OrderTaxes>(response);

            taxes.OrderTotalAmount.ShouldBe(11);
            taxes.TaxableAmount.ShouldBe(10);
            taxes.Rate.ShouldBe(0);
            taxes.Shipping.ShouldBe(1);
            taxes.AmountToCollect.ShouldBe(0);
            taxes.TaxSource.ShouldBe("destination");
            taxes.Country.ShouldBe("US");
            taxes.State.ShouldBe("FL");
            taxes.County.ShouldBe("HILLSBOROUGH");
            taxes.City.ShouldBe("CHEVAL");
            taxes.FreightTaxable.ShouldBeFalse();
        }

        [Fact]
        public async Task Invalid_customer_is_unsuccessful()
        {
            using var httpClient = _webApplicationFactory.CreateClient();

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
                ToZip = "33558",
                TaxExemptionType = "wholesale"
            };

            var requestContent = IntegrationTestHelper.GetRequestContent(query);

            var response = await httpClient.PostAsync("api/v1.0/taxes", requestContent);
            response.IsSuccessStatusCode.ShouldBeFalse();
        }

    }
}