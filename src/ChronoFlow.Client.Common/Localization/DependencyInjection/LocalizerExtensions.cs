using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace ChronoFlow.Client.Common.Localization.DependencyInjection;

internal static class LocalizerExtensions
{
    internal static IServiceCollection AddLocalizer(this IServiceCollection services, Action<LocalizerOptions> optionsAction)
    {
        services.AddLocalization();

        var options = new LocalizerOptions();
        optionsAction.Invoke(options);

        return services.AddScoped<ILocalizer>(sp =>
        {
            return new Localizer(
                sp.GetRequiredService<IStringLocalizerFactory>(),
                options);
        });
    }
}
