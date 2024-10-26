using ChronoFlow.Client.Common.Models;
using ChronoFlow.Shared.Common.Dtos;
using ChronoFlow.Shared.Common.Mapping.Configuration;

namespace ChronoFlow.Client.Common;

internal sealed class CommonMappingProfile : IMappingProfile
{
    public void ConfigureMapping(IMappingProfileConfiguration configuration)
    {
        configuration.ConfigureTypes<AddressModel, AddressDto>();
        configuration.ConfigureTypes<OptionalAddressModel, OptionalAddressDto>();
    }
}
