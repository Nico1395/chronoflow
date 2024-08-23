using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
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

public static class GetAllEmployees
{
    internal static IEndpointRouteBuilder UseGetAllEmployeesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("api/access-management/employees/get-all", async ([FromServices] IMediator mediator, [FromServices] IMapper mapper) =>
        {
            var result = await mediator.SendAsync(new GetAllEmployeesQuery());
            var mappedResult = mapper.MapResult<List<Employee>, List<EmployeeDto>>(result);
            
            return Results.Ok(mappedResult);
        });

        return endpoints;
    }

    public sealed record GetAllEmployeesQuery() : IQuery<Result<List<Employee>>>;

    internal sealed class GetAllEmployeesQueryHandler(IEmployeeReadRepository _employeeReadRepository) : IQueryHandler<GetAllEmployeesQuery, Result<List<Employee>>>
    {
        public async Task<Result<List<Employee>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeReadRepository.GetAllAsync(cancellationToken);
            return Result.Okay(employees);
        }
    }
}
