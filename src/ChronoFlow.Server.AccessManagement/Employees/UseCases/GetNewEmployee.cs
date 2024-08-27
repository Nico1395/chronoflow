using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.AccessManagement.Employees;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.AccessManagement.Employees.UseCases;

public static class GetNewEmployee
{
    internal static IEndpointRouteBuilder UseGetNewEmployeeEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("api/access-management/employees/get-new", async ([FromServices] IMediator mediator, [FromServices] IMapper mapper) =>
        {
            var result = await mediator.SendAsync(new GetNewEmployeeQuery());
            var mappedResult = mapper.MapResult<Employee, EmployeeDto>(result);

            return Results.Ok(mappedResult);
        });

        return endpoints;
    }

    public sealed record GetNewEmployeeQuery() : IQuery<Result<Employee>>;

    internal sealed class GetNewEmployeeQueryHandler : IQueryHandler<GetNewEmployeeQuery, Result<Employee>>
    {
        public Task<Result<Employee>> Handle(GetNewEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = new Employee();
            return Task.FromResult(Result.Okay(employee));
        }
    }
}
