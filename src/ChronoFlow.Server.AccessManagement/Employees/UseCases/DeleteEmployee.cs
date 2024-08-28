using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Employees.UseCases;

public static class DeleteEmployee
{
    [ApiController]
    public sealed class EmployeesController(IMediator _mediator) : ControllerBase
    {
        [HttpDelete("api/access-management/employees/delete")]
        public async Task<ActionResult<Result>> DeleteEmployeeAsync([FromQuery] Guid employeeId)
        {
            var result = await _mediator.SendAsync(new DeleteEmployeeCommand(employeeId));
            return Ok(result);
        }
    }

    public sealed record DeleteEmployeeCommand(Guid EmployeeId) : ICommand<Result>;

    private sealed class DeleteEmployeeCommandHandler(
        IEmployeeReadRepository _employeeReadRepository,
        IEmployeeWriteRepository _employeeWriteRepository,
        IUnitOfWork _unitOfWork) : ICommandHandler<DeleteEmployeeCommand, Result>
    {
        public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeReadRepository.GetByIdAsync(request.EmployeeId, cancellationToken);
            if (employee == null)
                return Result.NotFound();

            await _employeeWriteRepository.DeleteAsync(employee, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Okay();
        }
    }
}
