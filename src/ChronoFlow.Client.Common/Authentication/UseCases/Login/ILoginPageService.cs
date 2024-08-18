using ChronoFlow.Client.Common.Authentication.Entities;

namespace ChronoFlow.Client.Common.Authentication.UseCases.Login;

internal interface ILoginPageService
{
    internal Task<RequestLoginResult> LoginAsync(AuthenticationRequestViewModel authenticationRequest, CancellationToken cancellationToken = default);
}
