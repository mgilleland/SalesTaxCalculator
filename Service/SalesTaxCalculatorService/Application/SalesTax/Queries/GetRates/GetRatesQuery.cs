using MediatR;
using SalesTaxCalculatorService.Application.Common.Exceptions;
using SalesTaxCalculatorService.Application.Common.Interfaces;
using SalesTaxCalculatorService.Domain.Enums;
using SalesTaxCalculatorService.Domain.Models.Rates;
using System.Threading;
using System.Threading.Tasks;

namespace SalesTaxCalculatorService.Application.SalesTax.Queries.GetRates
{
    public class GetRatesQuery : IRequest<TaxRates>
    {
        public int? CustomerId { get; set; }
        public RatesRequest RatesRequest { get; set; }

        public class GetRatesQueryHandler : IRequestHandler<GetRatesQuery, TaxRates>
        {
            private readonly ITaxCalculatorFactory _taxCalculatorFactory;
            private readonly ICustomerRepository _customerRepository;

            public GetRatesQueryHandler(ITaxCalculatorFactory taxCalculatorFactory, ICustomerRepository customerRepository)
            {
                _taxCalculatorFactory = taxCalculatorFactory;
                _customerRepository = customerRepository;
            }

            public async Task<TaxRates> Handle(GetRatesQuery request, CancellationToken cancellationToken)
            {
                var taxServiceType = TaxServiceType.Default;

                if (request.CustomerId.HasValue)
                {
                    var customer = _customerRepository.GetCustomer(request.CustomerId.Value);
                    taxServiceType = customer.PreferredTaxEngine;
                }

                var calculator = _taxCalculatorFactory.TaxCalculator(taxServiceType);

                return await calculator.GetTaxRates(request.RatesRequest);
            }
        }
    }
}