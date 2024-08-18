namespace ChronoFlow.Client.Common.Authentication.UseCases.Login;

internal sealed record RequestLoginResult(
    RequestLoginResultCode Code,
    string? Message = null);
