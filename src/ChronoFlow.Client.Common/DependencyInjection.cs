using ChronoFlow.Client.Common.Localization.DependencyInjection;
using ChronoFlow.Client.Common.Localization.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoFlow.Client.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddLocalizer(options =>
        {
            options.AddResource<LocalizationResources>();
        });

        return services;
    }
}
