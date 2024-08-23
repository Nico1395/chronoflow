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

public static class GetEmployeeById
{
    internal static IEndpointRouteBuilder UseGetEmployeeByIdEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("api/access-management/employees/get-by-id", async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromQuery] Guid employeeId) =>
        {
            var result = await mediator.SendAsync(new GetEmployeeByIdQuery(employeeId));
            var mappedResult = mapper.MapResult<Employee, EmployeeDto>(result);

            return Results.Ok(mappedResult);
        });

        return endpoints;
    }

    public sealed record GetEmployeeByIdQuery(Guid EmployeeId) : IQuery<Result<Employee>>;

    internal sealed class GetEmployeeByIdQueryHandler(IEmployeeReadRepository _employeeReadRepository) : IQueryHandler<GetEmployeeByIdQuery, Result<Employee>>
    {
        public async Task<Result<Employee>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeReadRepository.GetByIdAsync(request.EmployeeId, cancellationToken);
            if (employee == null)
                return Result.NotFound<Employee>();

            return Result.Okay(employee);
        }
    }
}
