using ChronoFlow.Client.Common.Authentication.Entities;

namespace ChronoFlow.Client.Common.Authentication.UseCases.Login;

internal sealed class LoginPageService : ILoginPageService
{
    public Task<RequestLoginResult> LoginAsync(AuthenticationRequestViewModel authenticationRequest, CancellationToken cancellationToken)
    {
        return Task.FromResult(new RequestLoginResult(RequestLoginResultCode.Success));
    }
}
