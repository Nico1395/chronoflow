using ChronoFlow.Server.Common;
using System.Reflection;

namespace ChronoFlow.Server;

internal static class DependencyInjection
{
    internal static IServiceCollection AddChronoFlow(this IServiceCollection services)
    {
        var assemblies = GatherAssembliesToScan().ToArray();

        services.AddCommon(assemblies);

        return services;
    }

    private static IEnumerable<Assembly> GatherAssembliesToScan()
    {
        yield return Assembly.Load("ChronoFlow.Server");
        yield return Assembly.Load("ChronoFlow.Server.Common");
        yield return Assembly.Load("ChronoFlow.Server.AccessManagement");
    }
}
