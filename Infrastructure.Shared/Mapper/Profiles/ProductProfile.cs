using AutoMapper;

namespace Infrastructure.Shared.Mapper.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Persistence.Models.Product, Domain.Models.Product>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Price,y => y.MapFrom(z => z.Price.Price))
                .ForMember(x => x.DiscountMultiplier, y => y.MapFrom(z => z.Discount.Multiplier))
                .ForMember(x => x.QuantityAvailable, y => y.MapFrom(z => z.Stock.QuantityAvailable));

            CreateMap<Domain.Models.Product, Core.Application.DTOs.ProductResponse>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.QuantityAvailable, y => y.MapFrom(z => z.QuantityAvailable))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                .ForMember(x => x.QuantitySalePrices, y => y.Ignore());
        }
    }
}