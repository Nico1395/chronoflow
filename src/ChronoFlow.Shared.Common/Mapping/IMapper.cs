using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Shared.Common.Mapping;

public interface IMapper
{
    public TDestination Map<TDestination>(object source);
    public TDestination Map<TSource, TDestination>(TSource source);
    public Result<TDestination> Map<TSource, TDestination>(Result<TSource> result);
    public object Map(object source, Type sourceType, Type destinationType);
}
