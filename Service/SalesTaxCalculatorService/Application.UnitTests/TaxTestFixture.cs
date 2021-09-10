using Moq;
using SalesTaxCalculatorService.Application.Common.Exceptions;
using SalesTaxCalculatorService.Application.Common.Interfaces;
using SalesTaxCalculatorService.Application.SalesTax.Queries.CalculateTax;
using SalesTaxCalculatorService.Domain.Entities;
using SalesTaxCalculatorService.Domain.Enums;
using SalesTaxCalculatorService.Domain.Models.Rates;
using SalesTaxCalculatorService.Domain.Models.Taxes;
using Xunit;

namespace SalesTaxCalculatorService.Application.UnitTests
{
    public class TaxTestFixture
    {
        public TaxTestFixture()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(m => m.GetCustomer(1))
                .Returns(new Customer
                {
                    Id = 1, Name = "Test 1", PreferredTaxEngine = TaxServiceType.TaxJar
                });
            customerRepositoryMock.Setup(m => m.GetCustomer(99))
                .Throws(new NotFoundException("Customer", 99));
            CustomerRepository = customerRepositoryMock.Object;

            var mockRates = new UsTaxRates
            {
                State = "Florida",
                City = "Tampa",
                Country = "US",
                CityRate = 0,
                CombinedDistrictRate = 0,
                CombinedRate = 0.075F,
                CountryRate = 0,
                County = "Hillsborough",
                CountyRate = 0.015F,
                FreightTaxable = false,
                StateRate = 0.06F,
                Zip = "33614"
            };

            var mockOrderTaxes = new OrderTaxes
            {
                City = "Tampa",
                Country = "US",
                State = "FL",
                Shipping = 1,
                AmountToCollect = .75F,
                County = "Hillsborough",
                OrderTotalAmount = 10,
                Rate = .075F,
                TaxableAmount = 10,
                TaxSource = "destination",
                FreightTaxable = false
            };

            var taxCalculatorMock = new Mock<ITaxCalculator>();
            taxCalculatorMock.Setup(m => m.GetTaxRates(It.IsAny<RatesRequest>()))
                .ReturnsAsync(mockRates);
            taxCalculatorMock.Setup(m => m.CalculateTaxes(It.IsAny<CalculateTaxQuery>()))
                .ReturnsAsync(mockOrderTaxes);

            var factoryMock = new Mock<ITaxCalculatorFactory>();
            factoryMock.Setup(m => m.TaxCalculator(It.IsAny<TaxServiceType>()))
                .Returns(taxCalculatorMock.Object);

            TaxCalculatorFactory = factoryMock.Object;
        }

        public ITaxCalculatorFactory TaxCalculatorFactory { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
    }

    [CollectionDefinition("TaxTests")]
    public class TaxCollection : ICollectionFixture<TaxTestFixture> { }
}