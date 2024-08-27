using ChronoFlow.Server.AccessManagement;
using ChronoFlow.Server.Common;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using System.Reflection;

namespace ChronoFlow.Server;

internal static class DependencyInjection
{
    internal static IServiceCollection AddChronoFlow(this IServiceCollection services)
    {
        var assemblies = GatherAssembliesToScan().ToArray();

        services.AddCommon(assemblies);
        services.AddAccessManagement();

        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            })
            .ConfigureApplicationPartManager(setup =>
            {
                var controllerApplicationFeatureProvider = services.BuildServiceProvider().GetRequiredService<IApplicationFeatureProvider<ControllerFeature>>();
                setup.FeatureProviders.Add(controllerApplicationFeatureProvider);
            });

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
