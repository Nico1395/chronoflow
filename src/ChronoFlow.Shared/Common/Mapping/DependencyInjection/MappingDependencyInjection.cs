using ChronoFlow.Shared.Common.Mapping.Configuration;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChronoFlow.Shared.Common.Mapping.DependencyInjection;

public static class MappingDependencyInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMapster();
        services.AddTransient<IMapper, MapsterMapperAdapter>();

        var mappingProfileTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract || !t.IsInterface && t.IsAssignableTo(typeof(IMappingProfile)))
            .ToList();

        var profileConfigurations = new List<IMappingProfileConfiguration>();
        foreach (var mappingProfileType in mappingProfileTypes)
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
