using System.Net.Http.Headers;

namespace ChronoFlow.Client.Common.Http;

internal sealed class HttpClientProvider(IHttpClientFactory _httpClientFactory) : IHttpClientProvider
{
    private HttpClient? _serverHttpClient;

    public HttpClient GetServerClient()
    {
        return _serverHttpClient ??= CreateServerClient();
    }

    private HttpClient CreateServerClient()
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientConstants.Authorized);
        string? jwtToken = null;         // TODO -> Jwt token for server side using the auth state provider

        if (jwtToken != null)
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

        return httpClient;
    }
}
