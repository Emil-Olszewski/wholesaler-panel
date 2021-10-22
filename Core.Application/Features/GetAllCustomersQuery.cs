using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Infrastructure.Shared.Mapper;
using MediatR;

namespace Core.Application.Features
{
    public record GetAllCustomersQuery : IRequest<List<CustomerResponse>>
    {
        
    }

    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerResponse>>
    {
        private readonly IRepository repository;
        private readonly IMapperService mapper;

        public GetAllCustomersQueryHandler(IRepository repository, IMapperService mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task<List<CustomerResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return GetAllCustomers();
        }

        private async Task<List<CustomerResponse>> GetAllCustomers()
        {
            return mapper.Map<List<CustomerResponse>>(await repository.GetAllCustomersAsync());
        }
    }
}