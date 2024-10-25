namespace ChronoFlow.Shared.Common.Mapping;

public interface IMapper
{
    public TDestination Map<TDestination>(object source);
    public TDestination Map<TSource, TDestination>(TSource source);
    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    public object Map(object source, Type sourceType, Type destinationType);
    public object Map(object source, object destination, Type sourceType, Type destinationType);
}
