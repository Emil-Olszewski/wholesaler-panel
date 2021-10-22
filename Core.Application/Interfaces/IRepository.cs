using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Parameters;
using Domain.Models;

namespace Core.Application.Interfaces
{
    public interface IRepository
    {
        Task<Customer> GetCustomerByIdAsync(long id);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<List<Product>> GetAllProductsAsync(ProductParameters parameters);
        Task<List<QuantityDiscount>> GetAllQuantityDiscountsAsync();
    }
}