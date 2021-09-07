using AutoMapper;
using SalesTaxCalculatorService.Domain.Models;
using SalesTaxCalculatorService.Infrastructure.Services.TaxJar;

namespace SalesTaxCalculatorService.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Rate, TaxRates>();
        }
    }
}