using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace ChronoFlow.Client.Common.Http.DI;

internal static class HttpClientExtensions
{
    public static IServiceCollection AddHttp(this IServiceCollection services)
    {
        //// TODO -> Get from config or env variable for docker support
        //var serverAddress = Environment.GetEnvironmentVariable("VENTURE_SERVER_ADDRESS");
        //ArgumentNullException.ThrowIfNull(serverAddress, nameof(serverAddress));

        services.AddScoped<IHttpClientProvider, HttpClientProvider>();
        services.AddScoped<IServerHttpRequestService, ServerHttpRequestService>();

        services.AddHttpClient<HttpClientProvider>(HttpClientConstants.Authorized, b =>
        {
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");

            //b.BaseAddress = new Uri(serverAddress);
            b.BaseAddress = new Uri("https://localhost:7122");
            b.DefaultRequestHeaders.Accept.Add(mediaType);
        });

        return services;
    }
}
