using ChronoFlow.Client.Common.Layouts;
using ChronoFlow.Client.Common.Localization.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ChronoFlow.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<LayoutRouter>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddChronoFlow();

        var host = builder.Build();

        await host.InitializeAppCultureAsync();
        await host.RunAsync();
    }
}
