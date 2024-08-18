namespace ChronoFlow.Client.Common.Authentication.Entities;

internal sealed class AuthenticationSession
{
    public string JwtToken { get; set; } = string.Empty;
    public List<string> Permissions { get; set; } = [];
    public DateTime? Timestamp { get; set; }
}
