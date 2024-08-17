using ChronoFlow.Client.Common;

namespace ChronoFlow.Client;

internal static class DependencyInjection
{
    internal static IServiceCollection AddChronoFlow(this IServiceCollection services)
    {
        services.AddCommon();

        return services;
    }
}
