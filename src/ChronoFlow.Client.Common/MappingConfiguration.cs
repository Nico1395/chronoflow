using ChronoFlow.Client.Common.Objects.ValueObjects;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Objects.ValueObjects;
using Mapster;

namespace ChronoFlow.Client.Common;

internal sealed class MappingConfiguration : MappingConfigurationBase
{
    public override void Configure(TypeAdapterConfig mappingConfiguration)
    {
        mappingConfiguration.ConfigureTypes<AddressViewModel, AddressDto>();
    }
}
