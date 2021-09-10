namespace SalesTaxCalculatorService.Domain.Models.Rates
{
    public class RatesRequest
    {
        public RatesRequest(string country, string zip, string state, string city, string street)
        {
            Country = country;
            Zip = zip;
            State = state;
            City = city;
            Street = street;
        }

        public string Country { get; }
        public string Zip { get; }
        public string State { get; }
        public string City { get; }
        public string Street { get; }
    }
}