using Mapster;

namespace ChronoFlow.Shared.Common.Mapping;

public abstract class MappingConfigurationBase
{
    public abstract void Configure(TypeAdapterConfig mappingConfiguration);
}
