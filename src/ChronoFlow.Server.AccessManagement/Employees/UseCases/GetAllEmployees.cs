using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.AccessManagement.Employees;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Employees.UseCases;

public static class GetAllEmployees
{
    [ApiController]
    public sealed class EmployeesController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpGet("api/access-management/employees/get-all")]
        public async Task<ActionResult<Result<List<EmployeeDto>>>> GetAllEmployeesAsync()
        {
            var result = await _mediator.SendAsync(new GetAllEmployeesQuery());
            var mappedResult = _mapper.MapResult<List<Employee>, List<EmployeeDto>>(result);

            return Ok(mappedResult);
        }
    }

    public sealed record GetAllEmployeesQuery() : IQuery<Result<List<Employee>>>;

    private sealed class GetAllEmployeesQueryHandler(IEmployeeReadRepository _employeeReadRepository) : IQueryHandler<GetAllEmployeesQuery, Result<List<Employee>>>
    {
        public async Task<Result<List<Employee>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeReadRepository.GetAllAsync(cancellationToken);
            return Result.Okay(employees);
        }
    }
}
