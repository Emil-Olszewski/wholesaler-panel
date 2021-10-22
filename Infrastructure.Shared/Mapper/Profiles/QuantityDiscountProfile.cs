using AutoMapper;
using Domain.Models;

namespace Infrastructure.Shared.Mapper.Profiles
{
    public class QuantityDiscountProfile : Profile
    {
        public QuantityDiscountProfile()
        {
            CreateMap<Persistence.Models.QuantityDiscount, QuantityDiscount>()
                .ForMember(x => x.MinQuantity, y => y.MapFrom(z => z.MinQuantity))
                .ForMember(x => x.Multiplier, y => y.MapFrom(z => z.Multiplier));
        }
    }
}