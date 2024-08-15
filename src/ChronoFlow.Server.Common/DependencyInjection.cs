using ChronoFlow.Server.Common.Configuration;
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
}
