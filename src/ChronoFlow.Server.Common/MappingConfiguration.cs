using ChronoFlow.Server.Common.Objects.ValueObjects;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Objects.ValueObjects;
using Mapster;

namespace ChronoFlow.Server.Common;

internal sealed class MappingConfiguration : MappingConfigurationBase
{
    public override void Configure(TypeAdapterConfig mappingConfiguration)
    {
        mappingConfiguration.ConfigureTypes<Address, AddressDto>();
    }
}
