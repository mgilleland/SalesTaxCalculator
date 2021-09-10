using AutoMapper;
using SalesTaxCalculatorService.Application.Common.Interfaces;
using SalesTaxCalculatorService.Application.SalesTax.Queries.CalculateTax;
using SalesTaxCalculatorService.Domain.Models.Rates;
using SalesTaxCalculatorService.Domain.Models.Taxes;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SalesTaxCalculatorService.Infrastructure.Services.TaxJar
{
    public class TaxJarService : ITaxCalculator
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public TaxJarService(HttpClient client, IMapper mapper)
        {
            _httpClient = client;
            _mapper = mapper;
        }

        public async Task<TaxRates> GetTaxRates(RatesRequest request)
        {
            var url = new StringBuilder($"rates/{request.Zip}?country={request.Country}");

            if (!string.IsNullOrWhiteSpace(request.State))
            {
                url.Append($"&state={request.State}");
            }

            if (!string.IsNullOrWhiteSpace(request.City))
            {
                url.Append($"&city={request.City}");
            }

            if (!string.IsNullOrWhiteSpace(request.Street))
            {
                url.Append($"&street={request.Street}");
            }

            var response = await _httpClient.GetAsync(url.ToString());

            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();

            var rates = await JsonSerializer.DeserializeAsync<RatesResponse>(responseStream);

            return request.Country switch
            {
                "US" => _mapper.Map<Rate, UsTaxRates>(rates?.Rate),
                "CA" => _mapper.Map<Rate, CaTaxRates>(rates?.Rate),
                "AU" => _mapper.Map<Rate, AuTaxRates>(rates?.Rate),
                _ => _mapper.Map<Rate, EuTaxRates>(rates?.Rate),
            };
        }

        public async Task<OrderTaxes> CalculateTaxes(CalculateTaxQuery request)
        {
            var taxRequest = _mapper.Map<CalculateTaxQuery, TaxRequest>(request);

            var taxRequestJson = new StringContent(
                JsonSerializer.Serialize(taxRequest),
                Encoding.UTF8,
                "application/json");

            using var response =
                await _httpClient.PostAsync("taxes", taxRequestJson);

            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();

            var taxes = await JsonSerializer.DeserializeAsync<TaxResponse>(responseStream);

            var orderTaxes = _mapper.Map<Tax, OrderTaxes>(taxes?.Tax);

            return orderTaxes;
        }
    }
}