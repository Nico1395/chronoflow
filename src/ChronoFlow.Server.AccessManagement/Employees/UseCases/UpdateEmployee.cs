using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Shared.AccessManagement.Employees;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Employees.UseCases;

public static class UpdateEmployee
{
    [ApiController]
    public sealed class EmployeesController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpPatch("api/access-management/employees/update")]
        public async Task<ActionResult<Result>> UpdateEmployeeAsync([FromBody] EmployeeDto updatedEmployeeDto)
        {
            var updatedEmployee = _mapper.Map<Employee>(updatedEmployeeDto);
            var result = await _mediator.SendAsync(new UpdateEmployeeCommand(updatedEmployee));

            return Ok(result);
        }
    }

    public sealed record UpdateEmployeeCommand(Employee UpdatedEmployee) : ICommand<Result>;

    private sealed class UpdateEmployeeCommandHandler(
        IEmployeeReadRepository _employeeReadRepository,
        IEmployeeWriteRepository _employeeWriteRepository,
        IUnitOfWork _unitOfWork) : ICommandHandler<UpdateEmployeeCommand, Result>
    {
        public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeReadRepository.GetByIdAsync(request.UpdatedEmployee.Id, cancellationToken);
            if (existingEmployee == null)
                return Result.NotFound();

            await _employeeWriteRepository.UpdateAsync(
                existingEmployee,
                request.UpdatedEmployee,
                cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Okay();
        }
    }
}
