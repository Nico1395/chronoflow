using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage;

internal static class DependencyInjection
{
    internal static IServiceCollection AddMainDataMenu(this IServiceCollection services, params Assembly[] assemblies)
    {
        var mainDataMenuProfileTypes = assemblies.SelectMany(a => a.GetTypes()).Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(IMainDataMenuProfile)));
        foreach (var mainDataMenuProfileType in mainDataMenuProfileTypes)
            services.AddScoped(typeof(IMainDataMenuProfile), mainDataMenuProfileType);

        return services;
    }
}
