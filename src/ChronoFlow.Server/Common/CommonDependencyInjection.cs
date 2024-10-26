using ChronoFlow.Shared.Common.Mapping.DependencyInjection;
using System.Reflection;

namespace ChronoFlow.Server.Common;

public static class CommonDependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        var assemblies = YieldAssemblies().ToArray();

        services.AddMapping(options =>
        {
            options.ScanForProfilesInAssemblies(assemblies);
        });

        return services;
    }

    public static IEnumerable<Assembly> YieldAssemblies()
    {
        yield return Assembly.Load("ChronoFlow.Server");
    }
}
