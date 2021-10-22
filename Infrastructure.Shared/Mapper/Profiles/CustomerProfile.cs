using AutoMapper;
using Core.Application.DTOs;

namespace Infrastructure.Shared.Mapper.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Persistence.Models.Customer, Domain.Models.Customer>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.FullName))
                .ForMember(x => x.DiscountMultiplier, y => y.MapFrom(z => z.Discount.Multiplier));

            CreateMap<Domain.Models.Customer, CustomerResponse>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.FullName))
                .ForMember(x => x.DiscountMultiplier, y => y.MapFrom(z => z.DiscountMultiplier));
        }
    }
}