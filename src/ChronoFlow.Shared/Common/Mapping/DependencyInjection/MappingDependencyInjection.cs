using ChronoFlow.Shared.Common.Mapping.Adaptations;
using ChronoFlow.Shared.Common.Mapping.Configuration;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoFlow.Shared.Common.Mapping.DependencyInjection;

public static class MappingDependencyInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services, Action<MappingOptionsBuilder>? options = null)
    {
        services.AddMapster();
        services.AddTransient<IMapper, MapsterMapperAdapter>();

        var optionsBuilder = new MappingOptionsBuilder();
        options?.Invoke(optionsBuilder);
        var mappingOptions = optionsBuilder.Build();

        var profileConfigurations = new List<IMappingProfileConfiguration>();
        foreach (var mappingProfileType in mappingOptions.ProfileTypes)
        {
            var instance = Activator.CreateInstance(mappingProfileType);
            if (instance == null || instance is not IMappingProfile profile)
                continue;

            var configuration = new MappingProfileConfiguration();
            profile.ConfigureMapping(configuration);
            profileConfigurations.Add(configuration);
        }

        foreach (var typeMap in profileConfigurations.SelectMany(c => c.TypeMappingConfigurations))
        {
            TypeAdapterConfig.GlobalSettings.ForType(typeMap.SourceType, typeMap.DestinationType);
            TypeAdapterConfig.GlobalSettings.ForType(typeMap.DestinationType, typeMap.SourceType);
        }

        return services;
    }
}
