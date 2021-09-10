using SalesTaxCalculatorService.Application.SalesTax.Queries.CalculateTax;
using SalesTaxCalculatorService.Domain.Models.Rates;
using SalesTaxCalculatorService.Domain.Models.Taxes;
using System.Threading.Tasks;

namespace SalesTaxCalculatorService.Application.Common.Interfaces
{
    public interface ITaxCalculator
    {
        Task<TaxRates> GetTaxRates(RatesRequest request);
        Task<OrderTaxes> CalculateTaxes(CalculateTaxQuery request);
    }
}