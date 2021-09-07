using SalesTaxCalculatorService.Domain.Models;
using System.Threading.Tasks;

namespace SalesTaxCalculatorService.Application.Common.Interfaces
{
    public interface ITaxCalculator
    {
        Task<TaxRates> GetTaxRates(RatesRequest request);
    }
}