using Microsoft.Extensions.DependencyInjection;
using SalesTaxCalculatorService.Application.Common.Interfaces;
using SalesTaxCalculatorService.Infrastructure.Persistence;
using SalesTaxCalculatorService.Infrastructure.Services;

namespace SalesTaxCalculatorService.Infrastructure
{
    public static class DependencyInjection
    {
        public static object AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ITaxCalculatorFactory, TaxCalculatorFactory>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddAutoMapper(typeof(DependencyInjection));

            return services;
        }
    }
}