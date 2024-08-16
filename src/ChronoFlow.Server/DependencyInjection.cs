using ChronoFlow.Server.Common;
using ChronoFlow.Server.AccessManagement;
using System.Reflection;

namespace ChronoFlow.Server;

internal static class DependencyInjection
{
    internal static IServiceCollection AddChronoFlow(this IServiceCollection services)
    {
        var assemblies = GatherAssembliesToScan().ToArray();

        services.AddCommon(assemblies);
        services.AddAccessManagement();

        return services;
    }

    private static IEnumerable<Assembly> GatherAssembliesToScan()
    {
        yield return Assembly.Load("ChronoFlow.Server");
        yield return Assembly.Load("ChronoFlow.Server.Common");
        yield return Assembly.Load("ChronoFlow.Server.AccessManagement");
    }
}
