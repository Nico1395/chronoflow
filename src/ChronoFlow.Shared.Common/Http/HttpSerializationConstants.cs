using Newtonsoft.Json;

namespace ChronoFlow.Shared.Common.Http;

public static class HttpSerializationConstants
{
    public static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        NullValueHandling = NullValueHandling.Ignore,
    };
}
