using ChronoFlow.Server.Common.Configuration;
using ChronoFlow.Server.Common.Messaging.DependencyInjection;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Server.Common.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChronoFlow.Server.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection services, Assembly[] assemblies)
    {
        // TODO -> For Docker support find some way to differentiate between container and local installation.
        services.AddSingleton<IConfigurationProvider, AppsettingsConfigurationProvider>();
        var configurationProvider = services.BuildServiceProvider().GetRequiredService<IConfigurationProvider>();

        services.AddEfCore(configurationProvider, assemblies);
        services.AddMessaging(assemblies);
        services.AddServices(assemblies);

        return services;
    }

    private static IServiceCollection AddEfCore(this IServiceCollection services, IConfigurationProvider configurationProvider, Assembly[] assemblies)
    {
        var connectionString = configurationProvider.GetConnectionString();

        services.AddDbContext<DbContext, ChronoFlowDbContext>(options =>
        {
            options.UseNpgsql(connectionString);

            // Use the legacy Postgres timestamp behavior.
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        });

        services.AddDbContextModelOptions<ChronoFlowDbContext>(options =>
        {
            options.ScanForEntityConfigurationsInAssemblies(assemblies);
        });

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
