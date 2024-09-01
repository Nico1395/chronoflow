using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Shared.Common.Mapping;

internal sealed class Mapper(MapsterMapper.IMapper _mapper) : IMapper
{
    public TDestination Map<TDestination>(object source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TSource, TDestination>(source);
    }

    public object Map(object source, Type sourceType, Type destinationType)
    {
        return _mapper.Map(source, sourceType, destinationType);
    }

    public Result<TDestination> Map<TSource, TDestination>(Result<TSource> result)
    {
        return Map<Result<TDestination>>(result);
    }
}
