namespace ChronoFlow.Shared.Common.Mapping.Configuration;

internal sealed class MappingProfileConfiguration : IMappingProfileConfiguration
{
    public List<ITypeMappingConfiguration> TypeMappingConfigurations { get; } = [];

    public IMappingProfileConfiguration ConfigureTypes<TSource, TDestination>(Action<ITypeMappingExpression<TSource, TDestination>>? typeMap = null)
    {
        var expression = new TypeMappingExpression<TSource, TDestination>();
        typeMap?.Invoke(expression);
        TypeMappingConfigurations.Add(expression.Compile());

        return this;
    }
}
