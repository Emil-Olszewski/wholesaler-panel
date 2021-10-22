using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Application.Parameters;
using Infrastructure.Persistence.Storages;
using Infrastructure.Shared.Mapper;

namespace Infrastructure.Persistence.Repositories
{
    public class FakeRepository : IRepository
    {
        private readonly IMapperService mapper;
        private readonly FakeStorage storage;

        public FakeRepository(IMapperService mapper, FakeStorage storage)
        {
            this.mapper = mapper;
            this.storage = storage;
        }
        
        public Task<Domain.Models.Customer> GetCustomerByIdAsync(long id)
        {
            var result = storage.Customers.FirstOrDefault(x => x.Id == id);
            if (result is null)
            {
                throw new ResourceNotFoundException($"{typeof(Domain.Models.Customer)} Id:{id} does not exist.");
            }

            return Task.FromResult(mapper.Map<Domain.Models.Customer>(result));
        }

        public Task<List<Domain.Models.Customer>> GetAllCustomersAsync()
        {
            return Task.FromResult(mapper.Map<List<Domain.Models.Customer>>(storage.Customers));
        }

        public Task<List<Domain.Models.Product>> GetAllProductsAsync(ProductParameters parameters)
        {
            var result = storage.Products;

            if (parameters.MinQuantity != default)
            {
                result = result.Where(x => x.Stock.QuantityAvailable >= parameters.MinQuantity).ToList();
            }

            if (parameters.MaxPrice != default)
            {
                result = result.Where(x => parameters.MaxPrice > x.Price.Price).ToList();
            }

            return Task.FromResult(mapper.Map<List<Domain.Models.Product>>(result));
        }

        public Task<List<Domain.Models.QuantityDiscount>> GetAllQuantityDiscountsAsync()
        {
            return Task.FromResult(mapper.Map<List<Domain.Models.QuantityDiscount>>(storage.QuantityDiscounts));
        }
    }
}