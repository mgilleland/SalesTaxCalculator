using SalesTaxCalculatorService.Domain.Enums;

namespace SalesTaxCalculatorService.Application.Common.Interfaces
{
    public interface ITaxCalculatorFactory
    {
        ITaxCalculator TaxCalculator(TaxServiceType taxServiceType);
    }
}