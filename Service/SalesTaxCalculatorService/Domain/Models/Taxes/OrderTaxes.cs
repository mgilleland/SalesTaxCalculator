namespace SalesTaxCalculatorService.Domain.Models.Taxes
{
    public class OrderTaxes
    {
        public float AmountToCollect { get; set; }
        public float OrderTotalAmount { get; set; }
        public float Rate { get; set; }
        public float Shipping { get; set; }
        public string TaxSource { get; set; }
        public float TaxableAmount { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public bool FreightTaxable { get; set; }
        public string TaxExemptionType { get; set; }
    }
}