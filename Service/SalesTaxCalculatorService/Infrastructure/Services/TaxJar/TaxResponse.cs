using System.Text.Json.Serialization;

namespace SalesTaxCalculatorService.Infrastructure.Services.TaxJar
{
    public class TaxResponse
    {
        [JsonPropertyName("tax")]
        public Tax Tax { get; set; }
    }

    public class Tax
    {
        [JsonPropertyName("amount_to_collect")]
        public double AmountToCollect { get; set; }

        [JsonPropertyName("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonPropertyName("has_nexus")]
        public bool HasNexus { get; set; }

        [JsonPropertyName("jurisdictions")]
        public Jurisdictions Jurisdictions { get; set; }

        [JsonPropertyName("order_total_amount")]
        public double OrderTotalAmount { get; set; }

        [JsonPropertyName("rate")]
        public double Rate { get; set; }

        [JsonPropertyName("shipping")]
        public double Shipping { get; set; }

        [JsonPropertyName("tax_source")]
        public string TaxSource { get; set; }

        [JsonPropertyName("taxable_amount")]
        public double TaxableAmount { get; set; }

        [JsonPropertyName("exemption_type")]
        public string TaxExemptionType { get; set; }
    }

    public class Jurisdictions
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }
    }
}