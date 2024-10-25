using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Trailblazor.Routing.DependencyInjection;

namespace ChronoFlow.Client.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection services, params Assembly[] additionalAssemblies)
    {
        var assemblies = YieldAssemblies().Concat(additionalAssemblies).ToArray();

        services.AddTrailblazorRouting(options =>
        {
            options.AddProfilesFromAssemblies(assemblies);
            options.ScanForComponentsInAssemblies(assemblies);
        });

        return services;
    }

    public static IEnumerable<Assembly> YieldAssemblies()
    {
        yield return Assembly.Load("ChronoFlow.Client");
    }
}
