using AutoMapper;
using SalesTaxCalculatorService.Application.Common.Interfaces;
using SalesTaxCalculatorService.Domain.Models;
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

            return _mapper.Map<Rate, TaxRates>(rates?.Rate);
        }
    }
}