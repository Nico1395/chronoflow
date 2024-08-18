using ChronoFlow.Client.Common.Authentication.Entities;
using ChronoFlow.Client.Common.Localization;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Authentication.UseCases.Login;

public partial class LoginPage : ComponentBase
{
    private readonly AuthenticationRequestViewModel _authenticationRequest = new();

    [Inject]
    private ILoginPageService LoginPageService { get; set; } = null!;

    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    private void SetLogin()
    {
        _authenticationRequest.ClockIn = false;
    }

    private void SetClockIn()
    {
        _authenticationRequest.ClockIn = true;
    }

    private Task OnSubmitAsync()
    {
        return LoginPageService.LoginAsync(_authenticationRequest);
    }
}
