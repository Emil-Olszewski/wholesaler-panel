using System.Threading;
using System.Threading.Tasks;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Infrastructure.Shared.Mapper;
using MediatR;

namespace Core.Application.Features
{
    public record GetCustomerByIdQuery : IRequest<CustomerResponse>
    {
        public long CustomerId { get; init; }
    }
    
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponse>
    {
        private readonly IRepository repository;
        private readonly IMapperService mapper;

        public GetCustomerByIdQueryHandler(IRepository repository, IMapperService mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        
        public Task<CustomerResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return GetCustomerById(request.CustomerId);
        }

        private async Task<CustomerResponse> GetCustomerById(long customerId)
        {
            return mapper.Map<CustomerResponse>(await repository.GetCustomerByIdAsync(customerId));
        }
    }
}