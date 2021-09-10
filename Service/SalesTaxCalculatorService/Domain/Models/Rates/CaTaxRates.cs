namespace SalesTaxCalculatorService.Domain.Models.Rates
{
    public class CaTaxRates : TaxRates
    {
        public string Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public float CombinedRate { get; set; }
    }
}