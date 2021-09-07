using System.Text.Json.Serialization;

namespace SalesTaxCalculatorService.Infrastructure.Services.TaxJar
{
    public class RatesResponse
    {
        [JsonPropertyName("rate")]
        public Rate Rate { get; set; }
    }

    public class Rate
    {

        [JsonPropertyName("zip")]
        public string Zip { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("city_rate")]
        public string CityRate { get; set; }

        [JsonPropertyName("combined_district_rate")]
        public string CombinedDistrictRate { get; set; }

        [JsonPropertyName("combined_rate")]
        public string CombinedRate { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("country_rate")]
        public string CountryRate { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("county_rate")]
        public string CountyRate { get; set; }

        [JsonPropertyName("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("state_rate")]
        public string StateRate { get; set; }

        [JsonPropertyName("distance_sale_threshold")]
        public string DistanceSaleThreshold { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("parking_rate")]
        public string ParkingRate { get; set; }

        [JsonPropertyName("reduced_rate")]
        public string ReducedRate { get; set; }

        [JsonPropertyName("standard_rate")]
        public string StandardRate { get; set; }

        [JsonPropertyName("super_reduced_rate")]
        public string SuperReducedRate { get; set; }
    }
}