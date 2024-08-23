using ChronoFlow.Server.Common;
using ChronoFlow.Server.AccessManagement;
using System.Reflection;
using Newtonsoft.Json;

namespace ChronoFlow.Server;

internal static class DependencyInjection
{
    internal static IServiceCollection AddChronoFlow(this IServiceCollection services)
    {
        var assemblies = GatherAssembliesToScan().ToArray();

        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            })
            .AddApplicationPartsFromAssemblies(assemblies);

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

    private static IMvcBuilder AddApplicationPartsFromAssemblies(this IMvcBuilder mvcBuilder, Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
            mvcBuilder.AddApplicationPart(assembly);

        return mvcBuilder;
    }
}
