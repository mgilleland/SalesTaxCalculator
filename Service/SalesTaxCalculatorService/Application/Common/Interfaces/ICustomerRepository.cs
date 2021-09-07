using SalesTaxCalculatorService.Domain.Entities;

namespace SalesTaxCalculatorService.Application.Common.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(int customerId);
    }
}