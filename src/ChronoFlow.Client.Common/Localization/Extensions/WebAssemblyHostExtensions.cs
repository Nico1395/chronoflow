using ChronoFlow.Client.Common.Browser;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace ChronoFlow.Client.Common.Localization.Extensions;

public static class WebAssemblyHostExtensions
{
    public static async Task<WebAssemblyHost> InitializeAppCultureAsync(this WebAssemblyHost host)
    {
        var localStorage = host.Services.GetRequiredService<ILocalStorage>();

        try
        {
            var browserCulture = await localStorage.GetItemAsync("app-culture");
            var cultureInfo = CultureInfo.GetCultureInfo(browserCulture ?? "en-US");

            if (browserCulture == null)
                await localStorage.SetItemAsync("app-culture", cultureInfo.IetfLanguageTag);

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
        catch (Exception ex)
        {
            await host.Services.GetRequiredService<IBrowserLogger>().LogAsync(ex.Message);
        }

        return host;
    }
}
