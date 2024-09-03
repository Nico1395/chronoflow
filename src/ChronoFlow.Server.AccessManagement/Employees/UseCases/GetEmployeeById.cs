using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.AccessManagement.Employees;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Employees.UseCases;

public static class GetEmployeeById
{
    [ApiController]
    public sealed class EmployeesController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpGet("api/access-management/employees/get-by-id")]
        public async Task<ActionResult<Result<EmployeeDto>>> GetEmployeeByIdAsync([FromQuery] Guid employeeId)
        {
            var result = await _mediator.SendAsync(new GetEmployeeByIdQuery(employeeId));
            var mappedResult = _mapper.Map<Employee, EmployeeDto>(result);

            return Ok(mappedResult);
        }
    }

    public sealed record GetEmployeeByIdQuery(Guid EmployeeId) : IQuery<Result<Employee>>;

    private sealed class GetEmployeeByIdQueryHandler(IEmployeeReadRepository _employeeReadRepository) : IQueryHandler<GetEmployeeByIdQuery, Result<Employee>>
    {
        public async Task<Result<Employee>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeReadRepository.GetByIdEagerAsync(request.EmployeeId, cancellationToken);
            if (employee == null)
                return Result.NotFound<Employee>();

            return Result.Okay(employee);
        }
    }
}
