namespace ChronoFlow.Shared.Common.Mapping.Adaptations;

internal sealed class MapsterMapperAdapter(MapsterMapper.IMapper _mapsterMapper) : IMapper
{
    public TDestination Map<TDestination>(object source)
    {
        return _mapsterMapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapsterMapper.Map<TSource, TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapsterMapper.Map(source, destination);
    }

    public object Map(object source, Type sourceType, Type destinationType)
    {
        return _mapsterMapper.Map(source, sourceType, destinationType);
    }

    public object Map(object source, object destination, Type sourceType, Type destinationType)
    {
        return _mapsterMapper.Map(source, destination, sourceType, destinationType);
    }
}
