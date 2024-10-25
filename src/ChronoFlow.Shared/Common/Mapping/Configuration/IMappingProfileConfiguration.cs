namespace ChronoFlow.Shared.Common.Mapping.Configuration;

public interface IMappingProfileConfiguration
{
    internal List<ITypeMappingConfiguration> TypeMappingConfigurations { get; }
    public IMappingProfileConfiguration ConfigureTypes<TSource, TDestination>(Action<ITypeMappingExpression<TSource, TDestination>>? typeMap = null);
}
