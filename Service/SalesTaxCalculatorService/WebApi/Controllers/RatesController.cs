using Microsoft.AspNetCore.Mvc;
using SalesTaxCalculatorService.Application.SalesTax.Queries.GetRates;
using SalesTaxCalculatorService.Domain.Models.Rates;
using System.Threading.Tasks;

namespace SalesTaxCalculatorService.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/rates")]
    public class RatesController : ApiController
    {
        [HttpGet]
        public async Task<TaxRates> GetRates(int? customerId, string country, 
            string zip, string state, string city, string street)
        {
            var query = new GetRatesQuery
            {
                CustomerId = customerId,
                RatesRequest = new RatesRequest(country, zip, state, city, street)
            };

            return await Mediator.Send(query);
        }
    }
}
