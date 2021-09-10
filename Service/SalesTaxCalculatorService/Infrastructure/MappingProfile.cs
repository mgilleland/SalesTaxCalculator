using AutoMapper;
using SalesTaxCalculatorService.Application.SalesTax.Queries.CalculateTax;
using SalesTaxCalculatorService.Domain.Models.Rates;
using SalesTaxCalculatorService.Domain.Models.Taxes;
using SalesTaxCalculatorService.Infrastructure.Services.TaxJar;

namespace SalesTaxCalculatorService.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Rate, UsTaxRates>();
            CreateMap<Rate, CaTaxRates>();
            CreateMap<Rate, AuTaxRates>();
            CreateMap<Rate, EuTaxRates>();
            CreateMap<CalculateTaxQuery, TaxRequest>();
            CreateMap<Tax, OrderTaxes>().IncludeMembers(m => m.Jurisdictions);
            CreateMap<Jurisdictions, OrderTaxes>();
        }
    }
}