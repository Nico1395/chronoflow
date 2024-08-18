using ChronoFlow.Client.Common.Browser;
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

        services.AddScoped<ILocalStorage, LocalStorage>();
        services.AddScoped<ISessionStorage, SessionStorage>();
        services.AddScoped<IBrowserLogger, BrowserLogger>();

        // Validation
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<IValidationLocalizer, ValidationLocalizer>();

        return services;
    }
}
