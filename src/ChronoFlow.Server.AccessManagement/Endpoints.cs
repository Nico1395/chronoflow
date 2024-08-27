using ChronoFlow.Server.AccessManagement.Employees.UseCases;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.AccessManagement;

public static class Endpoints
{
    public static IEndpointRouteBuilder UseAccessManagementEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.UseGetAllEmployeesEndpoint();
        endpoints.UseGetEmployeeByIdEndpoint();
        endpoints.UseAddEmployeeEndpoint();
        endpoints.UseUpdateEmployeeEndpoint();
        endpoints.UseDeleteEmployeeEndpoint();
        endpoints.UseGetNewEmployeeEndpoint();

        return endpoints;
    }
}
