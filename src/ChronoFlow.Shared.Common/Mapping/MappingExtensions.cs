﻿using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChronoFlow.Shared.Common.Mapping;

public static class MappingExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddSingleton<MapsterMapper.IMapper>((serviceProvider) => new MapsterMapper.Mapper(TypeAdapterConfig.GlobalSettings));
        TypeAdapterConfig.GlobalSettings.AddConfigurationsFromAssemblies(assemblies);

        return services.AddSingleton<IMapper, Mapper>();
    }

    public static TypeAdapterConfig ConfigureTypes<TSource, TDestination>(this TypeAdapterConfig mappingConfiguration)
    {
        mappingConfiguration.ForType<TSource, TDestination>();
        mappingConfiguration.ForType<TDestination, TSource>();

        return mappingConfiguration;
    }

    public static TypeAdapterConfig ConfigureTypes(this TypeAdapterConfig mappingConfiguration, Type sourceType, Type destinationType)
    {
        mappingConfiguration.ForType(sourceType, destinationType);
        mappingConfiguration.ForType(destinationType, sourceType);

        return mappingConfiguration;
    }

    public static TypeAdapterConfig AddConfigurationsFromAssemblies(this TypeAdapterConfig mappingConfiguration, Assembly[] assemblies)
    {
        var configurations = assemblies.SelectMany(a => a.GetTypes().Where(t => t.IsAssignableTo(typeof(MappingConfigurationBase))));
        foreach (var configuration in configurations)
            mappingConfiguration.AddConfiguration(configuration);

        return mappingConfiguration;
    }

    private static TypeAdapterConfig AddConfiguration(this TypeAdapterConfig mappingConfiguration, Type mappingConfigurationType)
    {
        (Activator.CreateInstance(mappingConfigurationType)
            as MappingConfigurationBase ?? throw new InvalidCastException($"Type '{mappingConfigurationType.Name}' is not a descendand of '{typeof(MappingConfigurationBase).Name}'."))?
            .Configure(mappingConfiguration);

        return mappingConfiguration;
    }
}
