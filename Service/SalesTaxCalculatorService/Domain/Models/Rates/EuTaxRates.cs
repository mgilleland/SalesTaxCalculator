namespace SalesTaxCalculatorService.Domain.Models.Rates
{
    public class EuTaxRates : TaxRates
    {
        public string Name { get; set; }
        public float StandardRate { get; set; }
        public float ReducedRate { get; set; }
        public float SuperReducedRate { get; set; }
        public float ParkingRate { get; set; }
        public float DistanceSaleThreshold { get; set; }
    }
}