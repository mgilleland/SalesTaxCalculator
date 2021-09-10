using MediatR;
using SalesTaxCalculatorService.Domain.Enums;
using SalesTaxCalculatorService.Domain.Models.Taxes;
using System.Threading;
using System.Threading.Tasks;
using SalesTaxCalculatorService.Application.Common.Interfaces;

namespace SalesTaxCalculatorService.Application.SalesTax.Queries.CalculateTax
{
    public class CalculateTaxQuery : IRequest<OrderTaxes>
    {
        public int? CustomerId { get; set; }
        public string FromCountry { get; set; }
        public string FromZip { get; set; }
        public string FromState { get; set; }
        public string FromCity { get; set; }
        public string FromStreet { get; set; }
        public string ToCountry { get; set; }
        public string ToZip { get; set; }
        public string ToState { get; set; }
        public string ToCity { get; set; }
        public string ToStreet { get; set; }
        public float Amount { get; set; }
        public float Shipping { get; set; }
        public string TaxExemptionType { get; set; }

        public class CalculateTaxQueryHandler : IRequestHandler<CalculateTaxQuery, OrderTaxes>
        {
            private readonly ITaxCalculatorFactory _taxCalculatorFactory;
            private readonly ICustomerRepository _customerRepository;

            public CalculateTaxQueryHandler(ITaxCalculatorFactory taxCalculatorFactory, ICustomerRepository customerRepository)
            {
                _taxCalculatorFactory = taxCalculatorFactory;
                _customerRepository = customerRepository;
            }

            public async Task<OrderTaxes> Handle(CalculateTaxQuery request, CancellationToken cancellationToken)
            {
                var taxServiceType = TaxServiceType.Default;

                if (request.CustomerId.HasValue)
                {
                    var customer = _customerRepository.GetCustomer(request.CustomerId.Value);
                    taxServiceType = customer.PreferredTaxEngine;
                }

                var calculator = _taxCalculatorFactory.TaxCalculator(taxServiceType);

                return await calculator.CalculateTaxes(request);
            }
        }
    }
}