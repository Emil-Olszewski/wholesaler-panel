using AutoMapper;

namespace Infrastructure.Shared.Mapper
{
    public class MapperService : IMapperService
    {
        private readonly IMapper mapper;

        public MapperService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        
        public T Map<T>(object sourceObject)
        {
            return mapper.Map<T>(sourceObject);
        }

        public T Map<T>(object sourceObject, T destinationObject)
        {
            return mapper.Map(sourceObject, destinationObject);
        }
    }
}