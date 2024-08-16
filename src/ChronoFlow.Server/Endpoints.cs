using ChronoFlow.Server.Common;

namespace ChronoFlow.Server;

internal static class Endpoints
{
    internal static IEndpointRouteBuilder UseChronoFlowEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.UseCommonEndpoints();

        return endpoints;
    }
}
