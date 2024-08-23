namespace ChronoFlow.Client.Common.Http;

/// <summary>
/// Provider provides <see cref="HttpClient"/>s.
/// </summary>
public interface IHttpClientProvider
{
    /// <summary>
    /// Method provides an instance of <see cref="HttpClient"/> for the Venture backend.
    /// </summary>
    /// <remarks>
    /// Method authenticates the <see cref="HttpClient"/> if a user is authenticated.
    /// </remarks>
    /// <returns>Instance of <see cref="HttpClient"/> for the Venture backend</returns>
    public HttpClient GetServerClient();
}
