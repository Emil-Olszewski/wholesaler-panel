using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Application.Parameters;
using Infrastructure.Shared.Mapper;
using MediatR;

namespace Core.Application.Features
{
    public record GetAllProductsQuery : IRequest<List<ProductResponse>>
    {
        public long CustomerId { get; init; }
        public ProductParameters Parameters { get; init; }
    }
    
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
    {
        private readonly IMediator mediator;
        private readonly IRepository repository;
        private readonly IMapperService mapper;

        public GetAllProductsQueryHandler(IMediator mediator, IRepository repository, IMapperService mapper)
        {
            this.mediator = mediator;
            this.repository = repository;
            this.mapper = mapper;
        }
        
        public Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return GetAllProducts(request.CustomerId, request.Parameters);
        }

        private async Task<List<ProductResponse>> GetAllProducts(long customerId, ProductParameters parameters)
        {
            var customer = await repository.GetCustomerByIdAsync(customerId);
            var products = await repository.GetAllProductsAsync(parameters);
            var mappedProducts = mapper.Map<List<ProductResponse>>(products);

            foreach (var product in mappedProducts)
            {
                var countSalePriceCommand = new CountQuantitySalePricesCommand
                {
                    BasePrice = product.Price,
                    CustomerDiscountMultiplier = customer.DiscountMultiplier,
                    ProductDiscountMultiplier = products.First(x => x.Id == product.Id).DiscountMultiplier,
                };

                product.QuantitySalePrices = await mediator.Send(countSalePriceCommand);
            }

            return mappedProducts;
        }
    }
}