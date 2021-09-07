using SalesTaxCalculatorService.Domain.Enums;

namespace SalesTaxCalculatorService.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TaxServiceType PreferredTaxEngine { get; set; }
    }
}
