using Microsoft.AspNetCore.Mvc;
using SalesTaxCalculatorService.Application.SalesTax.Queries.CalculateTax;
using SalesTaxCalculatorService.Domain.Models.Taxes;
using System.Threading.Tasks;

namespace SalesTaxCalculatorService.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/taxes")]
    public class TaxesController : ApiController
    {
        [HttpPost]
        public async Task<OrderTaxes> CalculateTax([FromBody] CalculateTaxQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}