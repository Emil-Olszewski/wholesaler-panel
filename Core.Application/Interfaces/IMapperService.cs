namespace Infrastructure.Shared.Mapper
{
    public interface IMapperService
    {
        T Map<T>(object sourceObject);
        T Map<T>(object sourceObject, T destinationObject);
    }
}