namespace SalesTaxCalculatorService.Domain.Models
{
    public class TaxRates
    {
        public string Zip { get; set; }
        public string Country { get; set; }
        public float CountryRate { get; set; }
        public string State { get; set; }
        public float StateRate { get; set; }
        public string County { get; set; }
        public float CountyRate { get; set; }
        public string City { get; set; }
        public float CityRate { get; set; }
        public float CombinedDistrictRate { get; set; }
        public float CombinedRate { get; set; }
        public float StandardRate { get; set; }
        public float ReducedRate { get; set; }
        public float SuperReducedRate { get; set; }
        public float ParkingRate { get; set; }
        public float DistanceSaleThreshold { get; set; }
        public string Name { get; set; }
        public bool FreightTaxable { get; set; }
    }
}