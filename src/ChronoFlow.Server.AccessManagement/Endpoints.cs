using ChronoFlow.Server.AccessManagement.Roles.UseCases;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.AccessManagement;

public static class Endpoints
{
    public static IEndpointRouteBuilder UseAccessManagementEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.UseGetAllRolesEndpoint();
        endpoints.UseGetRoleByIdEndpoint();
        endpoints.UseAddRoleEndpoint();
        endpoints.UseUpdateRoleEndpoint();
        endpoints.UseDeleteRoleEndpoint();

        return endpoints;
    }
}
