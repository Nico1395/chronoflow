using ChronoFlow.Client.Common.Authentication;
using ChronoFlow.Client.Common.Authentication.UseCases.Login;
using ChronoFlow.Client.Common.Browser;
using ChronoFlow.Client.Common.Localization.DependencyInjection;
using ChronoFlow.Client.Common.Localization.Resources;
using ChronoFlow.Client.Common.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoFlow.Client.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        // Localization
        services.AddLocalizer(options =>
        {
            options.AddResource<LocalizationResources>();
        });

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

        return services;
    }
}
