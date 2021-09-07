using AutoMapper;
using SalesTaxCalculatorService.Application.Common.Interfaces;
using SalesTaxCalculatorService.Domain.Enums;
using SalesTaxCalculatorService.Infrastructure.Services.TaxJar;
using System.Net.Http;

namespace SalesTaxCalculatorService.Infrastructure.Services
{
    public class TaxCalculatorFactory : ITaxCalculatorFactory
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public TaxCalculatorFactory(HttpClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public ITaxCalculator TaxCalculator(TaxServiceType taxServiceType)
        {
            return taxServiceType switch
            {
                TaxServiceType.TaxJar => new TaxJarService(_client, _mapper),
                TaxServiceType.Default => new TaxJarService(_client, _mapper),
                _ => new TaxJarService(_client, _mapper)
            };
        }
    }
}