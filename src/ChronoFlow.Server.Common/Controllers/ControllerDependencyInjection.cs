using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChronoFlow.Server.Common.Controllers;

internal static class ControllerDependencyInjection
{
    internal static IServiceCollection AddApiEndpoints(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddSingleton(new ControllerOptionsBuilder().ScanInAssemblies(assemblies).Build());
        services.AddSingleton<IApplicationFeatureProvider<ControllerFeature>, ControllerApplicationFeatureProvider>();

        return services;
    }
}
