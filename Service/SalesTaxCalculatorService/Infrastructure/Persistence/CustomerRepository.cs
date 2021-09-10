using SalesTaxCalculatorService.Application.Common.Interfaces;
using SalesTaxCalculatorService.Domain.Entities;
using SalesTaxCalculatorService.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using SalesTaxCalculatorService.Application.Common.Exceptions;

namespace SalesTaxCalculatorService.Infrastructure.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers;

        public CustomerRepository()
        {
            // This is for demonstration purposes only.
            // In a production ready system the customer records would be
            // stored in database and retrieved with EF or some other means
            _customers = new List<Customer>();

            _customers.AddRange(new List<Customer>
            {
                new()
                {
                    Id = 1,
                    Name = "Acme Importers",
                    PreferredTaxEngine = TaxServiceType.TaxJar
                },
                new()
                {
                    Id = 2,
                    Name = "Betamax Liquidators",
                    PreferredTaxEngine = TaxServiceType.Default
                },
                new()
                {
                    Id = 3,
                    Name = "Charlie Sea Horses",
                    PreferredTaxEngine = TaxServiceType.TaxJar
                },
                new()
                {
                    Id = 4,
                    Name = "Darth Maul's Makeup",
                    PreferredTaxEngine = TaxServiceType.Default
                },
                new()
                {
                    Id = 5,
                    Name = "Exporters R Us",
                    PreferredTaxEngine = TaxServiceType.TaxJar
                }
            });
        }

        public Customer GetCustomer(int customerId)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
            {
                throw new NotFoundException("Customer", customerId);
            }

            return customer;
        }
    }
}