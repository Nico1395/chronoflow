using Microsoft.JSInterop;
using System.Globalization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoFlow.Client.Common.Localization.Extensions;

public static class WebAssemblyHostExtensions
{
    public static async Task<WebAssemblyHost> SetCultureAsync(this WebAssemblyHost host)
    {
        var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();

        try
        {
            var browserCulture = await jsRuntime.InvokeAsync<string>("cultureManager.get");
            var cultureInfo = CultureInfo.GetCultureInfo(browserCulture ?? "en-US");

            if (browserCulture == null)
                await jsRuntime.InvokeVoidAsync("cultureManager.set", cultureInfo.IetfLanguageTag);

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
        catch (Exception ex)
        {
            await jsRuntime.InvokeVoidAsync("console.log", ex.Message);
        }

        return host;
    }
}
