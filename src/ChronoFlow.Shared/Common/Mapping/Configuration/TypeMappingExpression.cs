namespace ChronoFlow.Shared.Common.Mapping.Configuration;

internal sealed class TypeMappingExpression<TSource, TDestination> : ITypeMappingExpression<TSource, TDestination>
{
    public ITypeMappingConfiguration Compile()
    {
        return new TypeMappingConfiguration()
        {
            SourceType = typeof(TSource),
            DestinationType = typeof(TDestination),
        };
    }
}
