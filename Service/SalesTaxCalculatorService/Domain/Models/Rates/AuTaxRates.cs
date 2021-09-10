namespace SalesTaxCalculatorService.Domain.Models.Rates
{
    public class AuTaxRates : TaxRates
    {
        public string Zip { get; set; }
        public float CountryRate { get; set; }
        public float CombinedRate { get; set; }
    }
}