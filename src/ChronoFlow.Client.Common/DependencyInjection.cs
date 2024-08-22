using ChronoFlow.Client.Common.Authentication;
using ChronoFlow.Client.Common.Authentication.UseCases.Login;
using ChronoFlow.Client.Common.Browser;
using ChronoFlow.Client.Common.Localization.DependencyInjection;
using ChronoFlow.Client.Common.Localization.Resources;
using ChronoFlow.Client.Common.MainData;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataPage.Configuration;
using ChronoFlow.Client.Common.Notifications;
using ChronoFlow.Client.Common.Processing.Search;
using ChronoFlow.Client.Common.Validation;
using ChronoFlow.Shared.Common.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChronoFlow.Client.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection services, Assembly[] assemblies)
    {
        // Localization
        services.AddLocalizer(options =>
        {
            options.AddResource<LocalizationResources>();
        });

        // Mapping
        services.AddMapper(assemblies);

        // Authentication
        services.AddCascadingAuthenticationState();
        services.AddAuthorizationCore();
        services.AddScoped<IAuthenticationStateProvider, AuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>();
        services.AddScoped<Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider>(sp => sp.GetRequiredService<AuthenticationStateProvider>());

        services.AddScoped<ILoginPageService, LoginPageService>();

        // Browser
        services.AddScoped<ILocalStorage, LocalStorage>();
        services.AddScoped<ISessionStorage, SessionStorage>();
        services.AddScoped<IBrowserLogger, BrowserLogger>();

        // Validation
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<IValidationLocalizer, ValidationLocalizer>();

        // Notifications
        services.AddScoped<ILocalNotificationPublisher, LocalNotificationPublisher>();
        services.AddScoped<ILocalNotificationManager, LocalNotificationManager>();

        // Main Data
        services.AddMainDataMenu(assemblies);
        services.AddScoped<ITimespanMessageCalculator, TimespanMessageCalculator>();

        // Processing
        services.AddScoped<ILocalSearchEngine, LocalSearchEngine>();

        return services;
    }
}
