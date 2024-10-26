using ChronoFlow.Server.Common.Entities;
using ChronoFlow.Shared.Common.Dtos;
using ChronoFlow.Shared.Common.Mapping.Configuration;

namespace ChronoFlow.Server.Common;

internal sealed class CommonMappingProfile : IMappingProfile
{
    public void ConfigureMapping(IMappingProfileConfiguration configuration)
    {
        configuration.ConfigureTypes<Address, AddressDto>();
        configuration.ConfigureTypes<OptionalAddress, OptionalAddressDto>();
    }
}
