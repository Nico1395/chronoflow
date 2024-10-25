namespace ChronoFlow.Shared.Common.Mapping.Configuration;

public interface ITypeMappingExpression<TSource, TDestination>
{
    internal ITypeMappingConfiguration Compile();
}
