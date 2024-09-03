using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.AccessManagement.Employees;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Employees.UseCases;

public static class GetNewEmployee
{
    [ApiController]
    public sealed class EmployeesController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("api/access-management/employees/get-new")]
        public async Task<ActionResult<Result<EmployeeDto>>> GetNewEmployeeAsync()
        {
            var result = await mediator.SendAsync(new GetNewEmployeeQuery());
            var mappedResult = mapper.Map<Employee, EmployeeDto>(result);

            return Ok(mappedResult);
        }
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
