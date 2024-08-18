using Microsoft.AspNetCore.Components.Authorization;

namespace ChronoFlow.Client.Common.Authentication;

public interface IAuthenticationStateProvider
{
    public Task<AuthenticationState> GetAuthenticationStateAsync();
}
