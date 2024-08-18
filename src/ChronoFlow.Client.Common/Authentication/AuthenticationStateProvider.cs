using Microsoft.AspNetCore.Components.Authorization;

namespace ChronoFlow.Client.Common.Authentication;

internal sealed class AuthenticationStateProvider : Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider, IAuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(new()));
    }
}
