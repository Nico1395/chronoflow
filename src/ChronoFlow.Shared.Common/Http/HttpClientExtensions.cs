using ChronoFlow.Shared.Common.Messaging;
using Newtonsoft.Json;

namespace ChronoFlow.Shared.Common.Http;

public static class HttpClientExtensions
{
    public static async Task<TResponse> DeserializeAsync<TResponse>(this HttpContent httpContent, CancellationToken cancellationToken = default)
    {
        var contentString = await httpContent.ReadAsStringAsync(cancellationToken);
        var deserializedContent = JsonConvert.DeserializeObject<TResponse>(contentString, HttpSerializationConstants.JsonSerializerSettings);

        ArgumentNullException.ThrowIfNull(deserializedContent, nameof(deserializedContent));
        return deserializedContent;
    }

    public static Task<Result<TResponseData>> DeserializeResultAsync<TResponseData>(this HttpContent httpContent, CancellationToken cancellationToken = default)
    {
        return httpContent.DeserializeAsync<Result<TResponseData>>(cancellationToken);
    }
}
