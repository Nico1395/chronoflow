using ChronoFlow.Client.AccessManagement;
using ChronoFlow.Client.Common;
using System.Reflection;

namespace ChronoFlow.Client;

internal static class DependencyInjection
{
    internal static IServiceCollection AddChronoFlow(this IServiceCollection services)
    {
        var assemblies = YieldAssemblies().ToArray();

        services.AddCommon(assemblies);
        services.AddAccessManagement();

        return services;
    }

    private static IEnumerable<Assembly> YieldAssemblies()
    {
        yield return Assembly.Load("ChronoFlow.Client");
        yield return Assembly.Load("ChronoFlow.Client.Common");
        yield return Assembly.Load("ChronoFlow.Client.AccessManagement");
    }
}
