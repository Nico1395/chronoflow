using ChronoFlow.Server.Common.Migrations.UseCases;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.Common;

public static class Endpoints
{
    public static IEndpointRouteBuilder UseCommonEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.UseMigrateMigrationsEndpoint();
        endpoints.UseGetAppliedMigrationsEndpoint();
        endpoints.UseGetPendingMigrationsEndpoint();

        return endpoints;
    }
}
