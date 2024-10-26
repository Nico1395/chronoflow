using ChronoFlow.Client.Common.Components.Pages;
using Trailblazor.Routing.Profiles;

namespace ChronoFlow.Client.Common;

internal sealed class CommonRoutingProfile : RoutingProfileBase
{
    protected override void Configure(RoutingProfileConfiguration configuration)
    {
        configuration.AddRoute<LandingPage>(p => p.WithUri("/"));
        //configuration.AddRoute<>(r => r
        //    .WithUri());
    }
}
