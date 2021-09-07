using Moq;
using SalesTaxCalculatorService.Application.Common.Interfaces;
using SalesTaxCalculatorService.Domain.Entities;
using SalesTaxCalculatorService.Domain.Enums;
using SalesTaxCalculatorService.Domain.Models;
using Xunit;

namespace Application.UnitTests
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

            CustomerRepository = customerRepositoryMock.Object;

            var mockRates = new TaxRates
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
                DistanceSaleThreshold = 0,
                FreightTaxable = false,
                Name = "Tampa",
                ParkingRate = 0,
                ReducedRate = 0,
                StandardRate = 0,
                StateRate = 0.06F,
                SuperReducedRate = 0,
                Zip = "33614"
            };

            var taxCalculatorMock = new Mock<ITaxCalculator>();
            taxCalculatorMock.Setup(m => m.GetTaxRates(It.IsAny<RatesRequest>()))
                .ReturnsAsync(mockRates);

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